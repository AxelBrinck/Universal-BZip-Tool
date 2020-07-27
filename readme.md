# Universal BZip Inflator Tool

## What is this?
This is a command line tool wrapping a class library for a very specific usage: decompressing a stream that was deflated by using a bzip algorithm.

We will see that not all the bzip streams are the same and how this tool can deal with two types of deflated bzip streams:
- bzip stream with no header.
- bzip stream prefixed with a two byte header.

If you used the bzip compressor bundled in a Node.js environment, the resulting stream will be prefixed by a header and the compression tool from .NET will not be able to read it. Microsoft stated that there will not be a fix for this anytime soon.

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

