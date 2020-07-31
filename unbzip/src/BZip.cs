using System.IO;
using System.IO.Compression;

namespace Unbzip
{
    public class BZip
    {
        private readonly Stream _inputStream;
        private readonly Stream _outputStream;

        public BZip(Stream inputStream, Stream outputStream)
        {
            _inputStream = inputStream;
            _outputStream = outputStream;
        }

        public void InflateFromPrefixedBZip() {
            Process(CompressionMode.Decompress, true);
        }

        public void InflateFromPureBZip() {
            Process(CompressionMode.Decompress, false);
        }

        public void Deflate() {
            Process(CompressionMode.Compress, false);
        }

        private void Process(CompressionMode mode, bool prefixedStream)
        {
            if (!_inputStream.CanRead) throw new IOException("The provided input stream does not allow reading!");
            if (!_outputStream.CanWrite) throw new IOException("The provided output stream does not allow writing!");
            
            if (prefixedStream) _inputStream.Seek(2, SeekOrigin.Begin);

            using (DeflateStream deflateStream = new DeflateStream (_inputStream, mode))
                deflateStream.CopyTo(_outputStream);
        }
    }
}