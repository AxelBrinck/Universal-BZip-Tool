# Universal BZip Tool

## What is this?

### Description

This is a standalone command line tool and a NuGet package for a very specific usage: inflating or deflating any bzip stream.

If you used the bzip compressor bundled in a Node.js environment, the resulting stream will be prefixed by a header, and the compression tool from .NET will not be able to read it. Microsoft stated that there will not be a fix for this anytime soon.

If there is a header in a deflated bzip stream, this header will occupy the first two bytes, then the actual compressed stream will start.

So this project provides the ability to perform the following thing: By seeking to the start of the third byte we avoid the header and Microsoft compression tool will be able to read it.

To gain compatibility with the two types of compressed streams (but sharing the same algorithm, bzip). We will use a try catch, trying to inflate the unmodified stream, and if it is not successful jumping to the first two bytes.

## How to use

### Package version

- Package Manager: `Install-Package decoder -Version 1.0.0`
- .NET CLI: `dotnet add package decoder --version 1.0.0`
- Package Reference: `<PackageReference Include="decoder" Version="1.0.0" />`
- Paket CLI: `paket add decoder --version 1.0.0`

When you have the package in your project go to the namespace **Unbzip**, and use the object BZip

### Standalone version

Once downloaded the zip you will see a bunch of files inside. Move those files to a secure place within your system and add the path to your environment variable.

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

## Contributions

Please if you feel that this project is missing something let me know! Im a little programmer here on GitHub and I want to learn from my mistakes! Thank you very much! :heart:
