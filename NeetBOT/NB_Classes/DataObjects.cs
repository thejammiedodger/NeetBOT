using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeetBOT.NB_Classes
{
    public class InsultData
    {
        public ObservableCollection<string> Languages;
        public ObservableCollection<string> Adjectives;
        public ObservableCollection<string> Insults;

        public InsultData() { }

        public InsultData(ObservableCollection<string> langs, ObservableCollection<string> adjs, ObservableCollection<string> insults)
        {
            Languages = langs;
            Adjectives = adjs;
            Insults = insults;
        }
    }
}
