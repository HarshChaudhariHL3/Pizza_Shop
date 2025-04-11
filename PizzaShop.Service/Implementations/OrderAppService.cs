using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class OrderAppService(IOrderAppRepository _orderAppRepository) : IOrderAppService
{
    public KotViewModel GetCategoryList()
    {
        var CategoryList = _orderAppRepository.GetCategoryList();

        var CategoryViewModelList = new KotViewModel
        {
            CategoryList = new List<KotCategory>()
        };
        foreach (var modifierGroup in CategoryList)
        {
            var CategoryViewModel = new KotCategory
            {
                CategoryId = modifierGroup.CategoryId,
                CategoryName = modifierGroup.CategoryName
            };
            CategoryViewModelList.CategoryList.Add(CategoryViewModel);
        }

        return CategoryViewModelList;
    }

    public TableOrderAppViewModel GetTableList()
    {
        var sectionListModel = _orderAppRepository.GetTableList();
        var allTables = sectionListModel.SelectMany(x => x.TableDetails).Where(x => x.IsDeleted == false).ToList();

        var viewModel = new TableOrderAppViewModel
        {
            SectionList = new List<SectionsViewModel>()
        };

        foreach (var section in sectionListModel)
        {
            var tablesInSection = allTables
                .Where(t => t.SectionId == section.SectionId)
                .Select(t => new TableViewModel
                {
                    TableId = t.TableId,
                    TableName = t.TableName,
                    SectionId = t.SectionId,
                    Capacity = t.Capacity,
                    Status = t.Status
                }).ToList();

            var sectionViewModel = new SectionsViewModel
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName,
                TableList = tablesInSection
            };

            viewModel.SectionList.Add(sectionViewModel);
        }

        return viewModel;
    }

    public List<Section> GetSections()
    {
        return _orderAppRepository.GetSections();
    }

    public void AddWaitingList(WaitingListViewModel model){
        _orderAppRepository.AddWaitingList(model);
    }


}
