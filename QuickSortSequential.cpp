#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <chrono>

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

// Sequential QuickSort
void quickSortSequential(vector<int>& arr, int low, int high) {
    if (low < high) {
        int pivot = partition(arr, low, high);
        quickSortSequential(arr, low, pivot - 1);
        quickSortSequential(arr, pivot + 1, high);
    }
}

int main() {
    int n = 10000; // Array size
    vector<int> arr(n);
    srand(time(0));
    
    for (int i = 0; i < n; i++) {
        arr[i] = rand() % 10000;
    }

    // Measure execution time for sequential QuickSort
    auto start = chrono::high_resolution_clock::now();
    quickSortSequential(arr, 0, n - 1);
    auto end = chrono::high_resolution_clock::now();
    chrono::duration<double> seqTime = end - start;
    cout << "Sequential QuickSort Time: " << seqTime.count() << " seconds" << endl;
    
    return 0;
}
