using System.IO;

namespace abacanet.diamond.infrastructure.Exceptions
{
    public class InvalidSheetException : IOException
    {
        private const string CustomMessage = "File {0} is invalid.";

        public InvalidSheetException(string file)
            :base(string.Format(CustomMessage, file))
        {
        }
    }
}
