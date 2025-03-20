#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <chrono>
#include <omp.h>

using namespace std;

// Function to partition the array
int partition(vector<int>& arr, int low, int high) {
    int pivot = arr[high];
    int i = low - 1;
    for (int j = low; j < high; j++) {
        if (arr[j] < pivot) {
            i++;
            swap(arr[i], arr[j]);
        }
    }
    swap(arr[i + 1], arr[high]);
    return i + 1;
}

// Parallel QuickSort using OpenMP
void quickSortParallel(vector<int>& arr, int low, int high) {
    if (low < high) {
        int pivot = partition(arr, low, high);
        #pragma omp parallel sections
        {
            #pragma omp section
            quickSortParallel(arr, low, pivot - 1);
            #pragma omp section
            quickSortParallel(arr, pivot + 1, high);
        }
    }
}

int main() {
    int n = 10000; // Array size
    vector<int> arr(n);
    srand(time(0));
    
    for (int i = 0; i < n; i++) {
        arr[i] = rand() % 10000;
    }

    // Measure execution time for parallel QuickSort
    auto start = chrono::high_resolution_clock::now();
    quickSortParallel(arr, 0, n - 1);
    auto end = chrono::high_resolution_clock::now();
    chrono::duration<double> parTime = end - start;
    cout << "Parallel QuickSort Time: " << parTime.count() << " seconds" << endl;
    
    return 0;
}
