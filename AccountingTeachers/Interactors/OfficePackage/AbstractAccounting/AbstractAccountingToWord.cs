using Interactors.OfficePackage.HelperEnums;
using Interactors.OfficePackage.Helpermodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.AbstractAccounting
{
    public abstract class AbstractAccountingToWord
    {
        public byte[]? CreateDoc(WordAccounting info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"Приказ(распоряжение) о прекращении(расторжении) трудового договора с работником(увольнении)", new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(info);


            return SaveWord(info);
        }
        protected abstract void CreateTable(WordAccounting info);
        protected abstract void CreateWord(WordAccounting info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract byte[]? SaveWord(WordAccounting info);
    }
}
