# CHOP-fMRU_Assistant

## Overview

CHOP-fMRU is a piece of medical software created at Children's Hospital of Philadelphia to perform functional MR urography and is based on the IDL framework. While implementing CHOP-fMRU at OHSU, I encountered several barriers that were best addressed with additional software. This adjunct program is designed to:
* Organize DICOM files from multiple studies
* Adjust DICOM header values to reflect assumptions made by CHOP-fMRU software and allow them to be imported into the program for analysis
* Selectively exclude certain time points or slice positions from the study
* Create new DICOM files from the images that are exported from CHOP-fMRU so they can be uploaded back to PACS as an additional series within the study.

## License

This open source project is available under the simplified BSD license. It relies on the Grassroots DICOM library, which is available under the same license.

## System Requirements

* .NET Framework 4.5 (minimum)
* 64-bit OS

## Getting Started

To quickly get started with the application, simply download the zip file containing the most recent stable release ([link to the zipfile in repository](https://github.com/overtone1000/CHOP-fMRU_Assistant/blob/master/CHOP-fMRU_Assistant_Release.zip)) and [download the file](https://github.com/overtone1000/CHOP-fMRU_Assistant/raw/master/CHOP-fMRU_Assistant_Release.zip). Extract the folder to your file system and run the **CHOP-fMRU_Assistant** application.

## Developers

This software is developed in C# using Visual Studio 2015. It's based on the 64-bit, C# wrapped version of the Grassroots DICOM library. Please feel free to submit issues through git or to contribute to the project yourself. Learning to collaborate using Git is the primary reason I decided to share this project.
