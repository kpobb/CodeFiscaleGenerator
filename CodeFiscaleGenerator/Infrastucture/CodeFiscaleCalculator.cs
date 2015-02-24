﻿using System;
using System.Linq;
using System.Text;
using CodeFiscaleGenerator.Entities;
using CodeFiscaleGenerator.Entities.Stub;

namespace CodeFiscaleGenerator.Infrastucture
{
    internal class CodeFiscaleCalculator
    {
        public string GenerateFiscaleCode(int labelId, int registrationId, int subRegistrationId)
        {

            var random = new Random();

            var code = new StringBuilder();

            // Brand
            code.Append(labelId == 1 ? "GIOCOD" : "BWINIT");

            // AAMS Code
            code.Append("0");

            code.Append(registrationId == 1024 ? 0 : 1);

            // AAMS Status
            code.Append("A");

            // Subregistration code
            code.Append("0");

            code.Append(subRegistrationId == 1024 ? 0 : 1);

            // Subregistration
            code.Append("S");

            // User number
            code.Append(random.Next(0, 9));
            code.Append(random.Next(0, 9));
            code.Append(random.Next(0, 9));

            // Random letter
            char[] letters =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G',
                'H', 'I', 'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
            };
            code.Append(letters[random.Next(1, 26)]);

            return code.ToString();
        }

        public bool IsCodeFiscaleUnique(StubResponse fiscaleCodes, string fiscaleCode)
        {
            return !(fiscaleCodes != null && fiscaleCodes.CodeFiscaleArray != null && 
                fiscaleCodes.CodeFiscaleArray.Any(s => s.FiscalCode.Equals(fiscaleCode, StringComparison.InvariantCultureIgnoreCase)));
        }

        public bool IsCodeFiscaleValid(string codeFiscale)
        {
            return codeFiscale.Length != 0;
        }
    }
}
