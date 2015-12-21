namespace LiveAdd.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Windows.Input;
    using Helpers;
    using Models;
    using Parse;

    public class AddViewModel : ViewModelBase
    {
        private ICommand acceptJobCommand;
        private ParseUser creator;
        private ParseUser worker;

        public AddViewModel()
        {
        }

        public static Expression<Func<AddModel, AddViewModel>> FromModel
        {
            get
            {
                return model => new AddViewModel()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Creator = model.Creator,
                    Worker = model.Worker,
                    ImgUrl = model.Image == null ? "http://www.stvhsaz.com/images/img3.jpg" : model.Image.Url.AbsoluteUri,
                    Address = model.Address
                };
            }
        }

        public ICommand AcceptJobCommand
        {
            get
            {
                if (this.acceptJobCommand == null)
                {
                    this.acceptJobCommand = new DelegateCommand(this.ExecuteAcceptJobCommand);
                }

                return this.acceptJobCommand;
            }
        }

        private async void ExecuteAcceptJobCommand()
        {
            if (this.Worker != null)
            {
                Notifier.ShowNotification("This ad already has a worker. Try another one :)");
            }
            else
            {
                try
                {
                    var ads = await new ParseQuery<AddModel>().FindAsync();

                    var adModel = ads.AsQueryable()
                                     .Where(a => a.Name == this.Name && a.Description == this.Description)
                                     .FirstOrDefault();

                    ParseUser creator = (await new ParseQuery<ParseUser>().FindAsync())
                                                                          .AsQueryable()
                                                                          .Where(u => u.ObjectId == adModel.Creator.ObjectId)
                                                                          .FirstOrDefault();

                    if (ParseUser.CurrentUser.Email == creator.Email)
                    {
                        throw new InvalidOperationException("You cannot accept your own ad. If you want to delete it - go to My Ads page");
                    }

                    adModel.Worker = ParseUser.CurrentUser;
                    this.Worker = ParseUser.CurrentUser;

                    await adModel.SaveAsync();

                    this.Worker = adModel.Worker;

                    Notifier.ShowNotification("You successfully accepted this ad.");
                }
                catch (Exception e)
                {
                    Notifier.ShowNotification(e.Message);
                }
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public double Price { get; set; }

        public string Address { get; set; }

        public ParseUser Creator
        {
            get { return this.creator; }
            set
            {
                this.creator = value;
                RaisePropertyChanged("Creator");
            }
        }

        public string CreatorPhone
        {
            get
            {
                var bla = (string)this.Creator["telephone"];
                return bla;
            }
        }

        public ParseUser Worker
        {
            get { return this.worker; }
            set
            {
                this.worker = value;
                RaisePropertyChanged("Worker");
            }
        }

        public string WorkerPhone
        {
            get
            {
                if (this.Worker == null)
                {
                    return string.Empty;
                }

                return (string)this.Worker["telephone"];
            }
        }
    }
}