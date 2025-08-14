using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Interactors.OfficePackage.AbstractAccounting;
using Interactors.OfficePackage.AbstractOrder;
using Interactors.OfficePackage.HelperEnums;
using Interactors.OfficePackage.Helpermodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.Implements
{
    public class SaveToWordAccounting : AbstractAccountingToWord
    {
        private WordprocessingDocument? _wordDocument;
        private Body? _docBody;
        private MemoryStream _mem = new MemoryStream();
        protected override void CreateParagraph(WordParagraph paragraph)
        {
            if (_docBody == null || paragraph == null)
            {
                return;
            }

            var docParagraph = new Paragraph();

            docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));

            foreach (var run in paragraph.Texts)
            {
                var docRun = new Run();

                var properties = new RunProperties();
                properties.AppendChild(new FontSize { Val = run.Item2.Size });
                if (run.Item2.Bold)
                {
                    properties.AppendChild(new Bold());
                }
                docRun.AppendChild(properties);

                docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });

                docParagraph.AppendChild(docRun);
            }

            _docBody.AppendChild(docParagraph);
        }

        protected override void CreateWord(WordAccounting    info)
        {
            _wordDocument = WordprocessingDocument.Create(_mem, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = _wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            _docBody = mainPart.Document.AppendChild(new Body());
        }

        protected override byte[]? SaveWord(WordAccounting info)
        {
            if (_docBody == null || _wordDocument == null)
            {
                return null;
            }

            _docBody.AppendChild(CreateSectionProperties());

            _wordDocument.MainDocumentPart!.Document.Save();
            _wordDocument.Dispose();

            return _mem.ToArray();
        }

        private static ParagraphProperties? CreateParagraphProperties(WordTextProperties? paragraphProperties)
        {
            if (paragraphProperties == null)
            {
                return null;
            }

            var properties = new ParagraphProperties();
            properties.AppendChild(new Justification
            {
                Val = GetJustificationValues(paragraphProperties.JustificationType)
            });

            var paragraphMarkRunProperties = new ParagraphMarkRunProperties();
            if (!string.IsNullOrEmpty(paragraphProperties.Size))
            {
                paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
            }
            properties.AppendChild(paragraphMarkRunProperties);

            return properties;
        }

        private static SectionProperties CreateSectionProperties()
        {
            var properties = new SectionProperties();
            var pageSize = new PageSize { Orient = PageOrientationValues.Portrait };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static JustificationValues GetJustificationValues(WordJustificationType type)
        {
            return type switch
            {
                WordJustificationType.Both => JustificationValues.Both,
                WordJustificationType.Center => JustificationValues.Center,
                WordJustificationType.Right => JustificationValues.Right,
                _ => JustificationValues.Left,
            };
        }


        protected override void CreateTable(WordAccounting info)
        {
            if (_docBody == null)
            {
                return;
            }

            var table = new Table();


            var tableProperties = new TableProperties(
                new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                )
            );
            table.AppendChild(tableProperties);


            var tableGrid = new TableGrid();
            tableGrid.AppendChild(new GridColumn { Width = "1000" });
            tableGrid.AppendChild(new GridColumn { Width = "1000" });
            tableGrid.AppendChild(new GridColumn { Width = "1000" });
            tableGrid.AppendChild(new GridColumn { Width = "1000" });
            tableGrid.AppendChild(new GridColumn { Width = "1000" });
            table.AppendChild(tableGrid);


            var headerRow = new TableRow();
            headerRow.AppendChild(CreateTableCell("№", true));
            headerRow.AppendChild(CreateTableCell("ФИО", true));
            headerRow.AppendChild(CreateTableCell("Ставка", true));
            headerRow.AppendChild(CreateTableCell("Должность", true));
            headerRow.AppendChild(CreateTableCell("Ученая степень, звание", true));
            table.AppendChild(headerRow);

         //логика для данных с таблички
            for( int i = 0; i < info.accounting.Count; ++i)
            {
                var row = new TableRow();
                row.AppendChild(CreateTableCell($"{i}"));
                row.AppendChild(CreateTableCell(info.accounting[i].FIO));
                row.AppendChild(CreateTableCell(info.accounting[i].Bet.ToString()));
                row.AppendChild(CreateTableCell(info.accounting[i].Position));
                row.AppendChild(CreateTableCell(info.accounting[i].Title));
            }

            _docBody.AppendChild(table);
        }

        private TableCell CreateTableCell(string text, bool isHeader = false)
        {
            var cell = new TableCell();
            var paragraph = new Paragraph();
            var run = new Run();
            var runProperties = new RunProperties();

            if (isHeader)
            {
                runProperties.AppendChild(new Bold());
            }

            run.AppendChild(runProperties);
            run.AppendChild(new Text(text));
            paragraph.AppendChild(run);
            cell.AppendChild(paragraph);

            return cell;
        }
    }
}
