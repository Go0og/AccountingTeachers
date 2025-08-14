using Interactors.OfficePackage.HelperEnums;
using Interactors.OfficePackage.Helpermodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.AbstractOrder
{
    public abstract class AbstractOrderHiringToWord
    {
        public byte[]? CreateDoc(WordOrder info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"Приказ о приеме работника на работу", new WordTextProperties { Bold = true, Size = "24" }) },
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
                {   ($"ПРИНЯТЬ НА РАБОТУ: ", new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"{info.teacher.FIO} с {info.teacher.DateStart} по {info.teacher.DateEnd} в 'ООО  Сочное Важное Офигенное (СВО)'", new WordTextProperties { Bold = true, Size = "24" }) },

                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {   ($"На позицию {info.teacher.PositionTeacher} при наличии ученой степени: {info.teacher.TitleTeacher} " +
                $"с тарифной ставкой(окладом): {info.teacher.bet}", new WordTextProperties { Bold = true, Size = "24" }) },

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
