using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekHub.Host.Logic;
using TekHub.Host.Models;

namespace TekHub.Host.ViewModel
{
    public partial class PlayerDefaultTabViewModel : ObservableObject
    {
        private readonly ObservableCollection<Config> _originalConfigs;

        [ObservableProperty]
        private ObservableCollection<Config> editableConfigs;

        public PlayerDefaultTabViewModel()
        {
            // Load original data from GameManager
            _originalConfigs = GameManager.Instance.DefaultPlayerAttribute;

            // Initialize editable copy
            LoadEditableConfigs("Gun");
        }

        public void LoadEditableConfigs(string filter)
        {
            EditableConfigs = new ObservableCollection<Config>(
                _originalConfigs
                    .Where(config => (filter == "Gun" && config.Value2 == "1") || (filter == "Vest" && config.Value2 == "0"))
                    .Select(config => new Config
                    {
                        Id = config.Id,
                        Name = config.Name,
                        CodeName = config.CodeName,
                        Value1 = config.Value1,
                        Value2 = config.Value2,
                        Description = config.Description
                    })
            );
        }

        public void SaveChanges()
        {
            foreach (var editedConfig in EditableConfigs)
            {
                var originalConfig = _originalConfigs.FirstOrDefault(c => c.Id == editedConfig.Id);
                if (originalConfig != null)
                {
                    originalConfig.Value1 = editedConfig.Value1;
                }
            }
        }

        public void DiscardChanges()
        {
            LoadEditableConfigs(EditableConfigs.Any(c => c.Value2 == "1") ? "Gun" : "Vest");
        }
    }
}
