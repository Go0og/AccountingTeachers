using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.Helpermodels
{
    public class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; } = new();

        public WordTextProperties? TextProperties { get; set; }
    }
}
