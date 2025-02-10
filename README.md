This project is to test the web page "https://e-commerce-kib.netlify.app/".

It includes four tests: Add, Edit, Delete, and Search.


To run the project:

1- Modify the path in line 18:
string logFilePath = @"C:\Users\U1\Desktop\pro1\LogsAndScreenshots\Log.txt";

2- Modify the path in line 84:
string filePath = @"C:\Users\U1\Desktop\pro1\p1\iphone.jpg";
Edit the path in line 84 to match your setup.(The image is included in the project under the name 'iphone.jpg')

3- Modify the path in line 232:
string screenshotPath = $@"C:\Users\U1\Desktop\pro1\LogsAndScreenshots\{fileName}";
This is the same as the first step; update it to match your environment.

To run the tests:
Open the terminal and run:

dotnet build

Then, run:

dotnet run

Results:
The test results can be found in the folder specified in points 1 and 3.
