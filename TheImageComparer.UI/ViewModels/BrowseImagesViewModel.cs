﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Helpers;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Models;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class BrowseImagesViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;
    private readonly IViewManagerService _viewManager;
    [ObservableProperty]
    private List<ImageUIModel> _images;

    public BrowseImagesViewModel(IImageComparerService comparerService, IViewManagerService viewManager)
    {
        _comparerService = comparerService;
        _viewManager = viewManager;
        Images = _comparerService
            .GetAllImages()
            .Result
            .Select(i => i.ToUiModel())
            .OrderByDescending(i => i.Score)
            .ThenBy(i => i.FilePath)
            .ToList();
    }
    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }

    [RelayCommand]
    private void ShowDetails(ImageModel image)
    {
        if(image is null)
            return;

        _viewManager.OpenView(ViewName.ImageDetails);
        WeakReferenceMessenger.Default.Send<ShowImageDetailsMessage>(new(image));
    }
}
