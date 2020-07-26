# Deflated JSON to JSON Converter tool

## What is this?
I had more than 300Gb of compressed raw JSON using GZIP. I needed this tool inflate all of them back to raw JSON in an automated way.

I am also switching IDEs from Intellij Rider to Visual Studio Code for C# and JS. I feel that this way I have more control about every aspect in my projects, so this will also serve as a training project.

## Technical Difficulties log

### Duplicate attributes error CS0579:
This happened when I was setting up a nested project. The moment when you are trying to build, if you do not specify all the Assembly Info, it will throw these errors.
```
error CS0579: Duplicate 'System.Reflection.AssemblyCompanyAttribute' attribute [...converter.csproj]
```

#### Solution
Please take a look at the .csproj files, you can see that for every AssemblyInfo field I either specify the value or set them to false. This way you will not end up having duplicate assembly information.