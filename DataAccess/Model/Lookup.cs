using System;
using System.Collections;
using System.Collections.Generic;


namespace DataAccess.Model
{// class name do not same as controller name, or ts get will not match correctly.
    public class CommonValue
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class CommonString
    {
        public string Value;
        public string Text;
    }

    public class LookupPack
    {
        public IEnumerable<CommonValue> Genre;
        public IEnumerable<CommonValue> AssignPurpose;
    }
}
