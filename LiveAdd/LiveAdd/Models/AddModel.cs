namespace LiveAdd.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Parse;

    [ParseClassName("AddModel")]
    public class AddModel : ParseObject
    {
        [ParseFieldName("creator")]
        public ParseUser Creator
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("worker")]
        public ParseUser Worker
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("price")]
        public double Price
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        [ParseFieldName("isActive")]
        public int IsActive
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("image")]
        public ParseFile Image
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [ParseFieldName("location")]
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
        }
    }
}
