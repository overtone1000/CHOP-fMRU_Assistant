CHOP-fMRU Assistant

CHOP-fMRU is a piece of medical software created at Children's Hospital of Philadelphia to perform functional MR urography and is based on the IDL framework.

While implementing CHOP-fMRU for my institution, I encountered several barriers that were best addressed with additional software. This adjunct program is designed to:
-Organize DICOM files from multiple studies
-Adjust DICOM header values to reflect assumptions made by CHOP-fMRU software and allow them to be imported into the study
-Create new DICOM files from the images that are exported from CHOP-fMRU so they can be uploaded back to a PACS

This project was developed in C# using Visual Studio Community 2015 and is available under the simplified BSD license. It is based on the Grassroots DICOM library, which is available under the same license.

I will create a more detailed wiki, which will be available in this git repository. For now, getting started will require:
- .NET Framework 4.5 (minimum)
- 64-bit OS

Tyler