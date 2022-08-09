# File sorter

## Overview

This solution consist of two projects:

- TestFileGenerator

    Generates a test file of a given number of lines with format:

    Number. Text

    Text can be repeated with a probability of 5%.

- Sorter


    Sorts input file using 3 actions

    - Splitting file into smaller ones (~100MB)
    - Sorting all the files separately
    - Merging sorted chunk files into one output file
  
    &nbsp;
    &nbsp;

    The output file would be in the same directory as input and with the same name with suffix "-sorted". For example, for input file:

        C:\repos\file.txt

    Output file would be:

        C:\repos\file-sorted.txt

## Bulding the solution

The project does not require any additional packages.

You can build the solution using Visual Studio or msbuild command:

    msbuild FileSorting.sln
    
## Running the programs

- TestFileGenerator

    It requires two parameters: 

    - number of lines in the output file
    - output file path

    &nbsp;
    &nbsp;

    Example usage:

        TestFileGenerator.exe 10000 output.txt


- Sorter

    It requires one parameter: 

    - input file path
  
    &nbsp;
    &nbsp;

    Example usage:
    
        Sorter.exe input.txt