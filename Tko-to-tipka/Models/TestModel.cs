using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tko_to_tipka.Models
{
    public class TestModel
    {
        public TestModel(String correctName, String recognizedName) {
            this.correctName = correctName;
            this.recognizedName = recognizedName;
            this.matches = correctName.Equals(recognizedName);
        }

        public String recognizedName { get; set; }
        public String correctName { get; set; }
        public bool matches { set;  get; }
    }
}