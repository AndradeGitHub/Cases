using System;
using System.Linq;
using System.Data.Entity.Validation;

namespace audatex.br.audabridge2.infrastructure.exception
{
    public class EntityValidationException
    {
        public static Tuple<string, DbEntityValidationException> Validate(DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

            return new Tuple<string, DbEntityValidationException>(exceptionMessage, ex);
        }
    }
}
