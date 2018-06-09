using System;
using System.ComponentModel.DataAnnotations;

namespace IrregularVerbs
{
    public class Verb
    {
        [Key]
        public int Id { get; set; }
        public string BaseForm { get; set; }
        public string PastSimple { get; set; }
        public string PastParticiple { get; set; }

        [NonSerialized] public bool Good = false;
    }
}