using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Web.Models
{
    public interface ICache
    {
        IConfigurationRoot Config { get; set; }
        int CSSHash { get; }
        string TitleImage { get; }
        string TitleImageXS { get; }
        IList<Page> Pages { get; }
        string Intro { get; }
        string AvailabilityMessage { get; }
        string RSS { get; }
        string Atom { get; }
        Task Refresh();
    }
}