# Deflated JSON to JSON Converter tool

## What is this?
I had more than 300Gb of compressed raw JSON using GZIP. I needed this tool inflate all of them back to raw JSON in an automated way.

I am also switching IDEs from Intellij Rider to Visual Studio Code for C# and JS. I feel that this way I have more control about every aspect in my projects, so this will also serve as a training project.

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