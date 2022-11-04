using System.Collections.Generic;

namespace MilitaryElite.Models.Interfaces
{
    public interface ILieutenantGeneral
    {
        IReadOnlyCollection<IPrivate> Privates { get;}
    }
}
