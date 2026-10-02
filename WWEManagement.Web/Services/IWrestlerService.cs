using WWEManagement.Data.Models;
using WWEManagement.Web.Dtos;
using WWEManagement.Web.ViewModels;

namespace WWEManagement.Web.Services;

public interface IWrestlerService
{
    List<Wrestler> GetAll();

    List<WrestlerDetails> GetAllDetails();

    WrestlerDetails? GetById(int id);

    int Create(
        CreateWrestlerViewModel model);

    int Create(
        WrestlerRequestDto request);

    bool Update(
        EditWrestlerViewModel model);

    bool Update(
        int id,
        WrestlerRequestDto request);

    bool Delete(int id);
}