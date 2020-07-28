# Universal BZip Tool

## What is this?
This is a command line tool wrapping a class library for a very specific usage: inflating or deflating a bzip stream.

It is oriented to batch-process a folder containing the file streams.

We will see that not all the bzip streams are the same, and how this tool can deal with two types of deflated bzip streams:
- bzip stream with no header.
- bzip stream prefixed with a two byte header.

If you used the bzip compressor bundled in a Node.js environment, the resulting stream will be prefixed by a header and the compression tool from .NET will not be able to read it. Microsoft stated that there will not be a fix for this anytime soon.

## How to use

Use the command -help to display instructions:
```
Universal BZip Tool 1.0.0.0, 27 Jul 2020, Axel Brinck.
Can inflate all bzip stream types.
Tool oriented to batch process files from a given directory.

USAGE:
Commands: -inflate / -deflate / -help
Argument 1: Source directory. (All files must contain a bzip stream)
Argument 2: Target directory.
Argument 3: Extension. (Specify the extension the file h)

EXAMPLES:
unbzip.exe -inflate compressed_files/ raw_files/ .deflated
unbzip.exe -deflate raw_files/ compressed_files/ .deflated
unbzip.exe -help

EXTENSION BEHAVIOUR NOTES:
Deflating will append the specified extension to the original file names
Inflating will remove the specified extension to the deflated file names
```

## Download

Downloads can be found in this repository. Check **releases**.

## Installation

Once downloaded the zip you will see a bunch of files inside. Move those files to a secure place within your system and add the path to your environment variable.

## Releases

### **1.0.0.0b** - Source code match

#### Changelog

+ Source code attached matches the binary.
+ Platform is now specified.

### **1.0.0.0** - Initial release

#### Main features

+ Can deflate files.
+ Can inflate from non-prefixed bzip streams.
+ Can inflate from prefixed bzip streams.

#### Known bugs

+ **B001**: Trying to inflate a non bzip or corrupted stream will cause an exception that is not handled and the program will suddenly exit.

## Possible features in future updates

- Drag and drop folder to start batch processing.
- Make the code non-procedural.
- Parallel processing.
- Watch feature. *(Watch for modified/new files in a folder).*
- Generate log file on error.

*This project already served my needs, please if you like it and you feel that it could go beyond feel free to contribute as it is possible that will no longer be maintained.*

## Technical Difficulties log

### **Duplicate attributes error CS0579:**

#### Description
This happened when I was setting up a nested project. The moment when you are trying to build, if you do not specify all the Assembly Info, it will throw these errors.
```
error CS0579: Duplicate 'System.Reflection.AssemblyCompanyAttribute' attribute [...converter.csproj]
```
You will reach to a point when the TargetFramework attribute is duplicated. You will be unable to build unless you **delete obj folders**.

#### Solution
Deleting all the time obj folders is ofcourse not a fix. To deal with **CS0579** it is actually quite simple:

**Do not design your folder structure with nested projects.**

The reasoning is because when dotnet is building the project it will lookup for building information not only coming from the .csproj file in the root dir, it will also for some reason, look up into subfolders, gathering then duplicated meta data to just build one project.

If you search for this error on the web, people recommend you to just add the following info in your .csproj file:
```
<GenerateAssemblyInfo>false</GenerateAssemblyInfo>
```
This will prevent dotnet from generating any assembly info. But it has the cost of not being able to have any meta data in your project. 

**Use separate folders for each project instead.**

### **Microsoft decompression tool is not able to read bzip headers:**

#### Description

Some bzip compressors add some headers to the deflated stream in order to specify certain parameters or compression strategies that was used.

The compression tool that it is bundled within .NET is not able to read any header.

Searching on the internet for a solution I founded that I would need another compression library to achieve this. But luckily I end up understanding what was exactly the problem with the headers.

#### Solution

If there is a header in a deflated bzip stream, this header will occupy the first two bytes, then the actual compressed stream will start.

By seeking to the start of the third byte we avoid the header and Microsoft compression tool will be able to read it.

To gain compatibility with the two types of compressed streams (but sharing the same algorithm, bzip). We will use a try catch, trying to inflate the unmodified stream, and if it is not successful jumping to the first two bytes.

