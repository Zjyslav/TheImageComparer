﻿using TheImageComparer.UI.Helpers.StartupHelpers;
using TheImageComparer.UI.Services;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Factories;
public class ViewFactory : IViewFactory
{
    private readonly IAbstractFactory<DatabaseMenuView> _databaseMenuFactory;
    private readonly IAbstractFactory<OpenFolderView> _openFolderFactory;
    private readonly IAbstractFactory<BrowseImagesView> _browseImagesFactory;
    private readonly IAbstractFactory<VoteView> _voteFactory;

    public ViewFactory(IAbstractFactory<DatabaseMenuView> databaseMenuFactory,
                       IAbstractFactory<OpenFolderView> openFolderFactory,
                       IAbstractFactory<BrowseImagesView> browseImagesFactory,
                       IAbstractFactory<VoteView> voteFactory)
    {
        _databaseMenuFactory = databaseMenuFactory;
        _openFolderFactory = openFolderFactory;
        _browseImagesFactory = browseImagesFactory;
        _voteFactory = voteFactory;
    }

    public IView? CreateView(ViewName viewName)
    {
        switch (viewName)
        {
            case ViewName.DatabaseMenu:
                return _databaseMenuFactory.Create();
            case ViewName.OpenFolder:
                return _openFolderFactory.Create();
            case ViewName.BrowseImages:
                return _browseImagesFactory.Create();
            case ViewName.Vote:
                return _voteFactory.Create();
            default:
                return null;
        }
    }
}
