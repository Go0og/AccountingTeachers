using Interactors.OfficePackage.HelperEnums;
using Interactors.OfficePackage.Helpermodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.AbstractOrder
{
    public abstract class AbstractOrderFiringToWord
    {
        public byte[]? CreateDoc(WordOrder info)
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
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"документ №{info.order.Id} дата формирования: {info.order.DateOrders}", new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Right
                }
            });
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"Прекратить действие трудового договора от {info.teacher.DateStart}", new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"Уволить  {info.teacher.DateEnd}", new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"{info.teacher.FIO} с должности '{info.teacher.PositionTeacher}'", new WordTextProperties { Bold = true, Size = "24" }) },

                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });


            return SaveWord(info);
        }

        protected abstract void CreateWord(WordOrder info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract byte[]? SaveWord(WordOrder info);

    }
}
