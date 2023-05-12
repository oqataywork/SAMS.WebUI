using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SAMS.Model;
using SAMS.WebUI.Helpers;
using Newtonsoft.Json;
using SAMS.Core.ViewModels;
using SAMS.Core.Helpers;

namespace SAMS.WebUI.Services.Partials
{
    public static class ShouldRefreshService
    {
        //

        private static bool _ShouldRefreshAssetOperationIndicatorsMaps;
        public static bool GetShouldRefreshAssetOperationIndicatorsMaps()
        {
            return _ShouldRefreshAssetOperationIndicatorsMaps;
        }
        public static void SetShouldRefreshAssetOperationIndicatorsMaps(bool v)
        {
            _ShouldRefreshAssetOperationIndicatorsMaps = v;
        }

        private static bool _ShouldRefreshGetCategoriesTree;
        public static bool GetShouldRefreshGetCategoriesTree()
        {
            return _ShouldRefreshGetCategoriesTree;
        }
        public static void SetShouldRefreshGetCategoriesTree(bool v)
        {
            _ShouldRefreshGetCategoriesTree=v;
        }

        private static bool _ShouldRefreshAssetAttributeCategorys;

        public static bool GetShouldRefreshAssetAttributeCategorys()
        {
            return _ShouldRefreshAssetAttributeCategorys;
        }
        public static void SetShouldRefreshAssetAttributeCategorys(bool arg)
        {
            _ShouldRefreshAssetAttributeCategorys = arg;
        }


        private static bool _ShouldRefreshAssetAttributeMaps;

        public static bool GetShouldRefreshAssetAttributeMaps()
        {
            return _ShouldRefreshAssetAttributeMaps;
        }
        public static void SetShouldRefreshAssetAttributeMaps(bool arg)
        {
            _ShouldRefreshAssetAttributeMaps = arg;
        }


        private static bool _ShouldRefreshAssetAttributes;

        public static bool GetShouldRefreshAssetAttributes()
        {
            return _ShouldRefreshAssetAttributes;
        }
        public static void SetShouldRefreshAssetAttributes(bool arg)
        {
            _ShouldRefreshAssetAttributes = arg;
        }


        private static bool _ShouldRefreshAssetCategorys;

        public static bool GetShouldRefreshAssetCategorys()
        {
            return _ShouldRefreshAssetCategorys;
        }
        public static void SetShouldRefreshAssetCategorys(bool arg)
        {
            _ShouldRefreshAssetCategorys = arg;
            if (arg)
                SetShouldRefreshGetCategoriesTree(true);
        }


        private static bool _ShouldRefreshAssetCharacteristics;

        public static bool GetShouldRefreshAssetCharacteristics()
        {
            return _ShouldRefreshAssetCharacteristics;
        }
        public static void SetShouldRefreshAssetCharacteristics(bool arg)
        {
            _ShouldRefreshAssetCharacteristics = arg;
        }


        private static bool _ShouldRefreshAssetDocuments;

        public static bool GetShouldRefreshAssetDocuments()
        {
            return _ShouldRefreshAssetDocuments;
        }
        public static void SetShouldRefreshAssetDocuments(bool arg)
        {
            _ShouldRefreshAssetDocuments = arg;
        }


        private static bool _ShouldRefreshAssets;

        public static bool GetShouldRefreshAssets()
        {
            return _ShouldRefreshAssets;
        }
        public static void SetShouldRefreshAssets(bool arg)
        {
            _ShouldRefreshAssets = arg;
        }


        private static bool _ShouldRefreshAssetServiceIntervals;

        public static bool GetShouldRefreshAssetServiceIntervals()
        {
            return _ShouldRefreshAssetServiceIntervals;
        }
        public static void SetShouldRefreshAssetServiceIntervals(bool arg)
        {
            _ShouldRefreshAssetServiceIntervals = arg;
        }


        private static bool _ShouldRefreshAssetStatusHistorys;

        public static bool GetShouldRefreshAssetStatusHistorys()
        {
            return _ShouldRefreshAssetStatusHistorys;
        }
        public static void SetShouldRefreshAssetStatusHistorys(bool arg)
        {
            _ShouldRefreshAssetStatusHistorys = arg;
        }


        private static bool _ShouldRefreshAssetTypeImages;

        public static bool GetShouldRefreshAssetTypeImages()
        {
            return _ShouldRefreshAssetTypeImages;
        }
        public static void SetShouldRefreshAssetTypeImages(bool arg)
        {
            _ShouldRefreshAssetTypeImages = arg;
        }


        private static bool _ShouldRefreshAssetTypes;

        public static bool GetShouldRefreshAssetTypes()
        {
            return _ShouldRefreshAssetTypes;
        }
        public static void SetShouldRefreshAssetTypes(bool arg)
        {
            _ShouldRefreshAssetTypes = arg;
        }


        private static bool _ShouldRefreshCharacteristicCategorys;

        public static bool GetShouldRefreshCharacteristicCategorys()
        {
            return _ShouldRefreshCharacteristicCategorys;
        }
        public static void SetShouldRefreshCharacteristicCategorys(bool arg)
        {
            _ShouldRefreshCharacteristicCategorys = arg;
        }


        private static bool _ShouldRefreshCharacteristics;

        public static bool GetShouldRefreshCharacteristics()
        {
            return _ShouldRefreshCharacteristics;
        }
        public static void SetShouldRefreshCharacteristics(bool arg)
        {
            _ShouldRefreshCharacteristics = arg;
        }


        private static bool _ShouldRefreshDepartments;

        public static bool GetShouldRefreshDepartments()
        {
            return _ShouldRefreshDepartments;
        }
        public static void SetShouldRefreshDepartments(bool arg)
        {
            _ShouldRefreshDepartments = arg;
        }


        private static bool _ShouldRefreshDocumentTypes;

        public static bool GetShouldRefreshDocumentTypes()
        {
            return _ShouldRefreshDocumentTypes;
        }
        public static void SetShouldRefreshDocumentTypes(bool arg)
        {
            _ShouldRefreshDocumentTypes = arg;
        }


        private static bool _ShouldRefreshLocations;

        public static bool GetShouldRefreshLocations()
        {
            return _ShouldRefreshLocations;
        }
        public static void SetShouldRefreshLocations(bool arg)
        {
            _ShouldRefreshLocations = arg;
        }


        private static bool _ShouldRefreshMaintenanceRepairTypes;

        public static bool GetShouldRefreshMaintenanceRepairTypes()
        {
            return _ShouldRefreshMaintenanceRepairTypes;
        }
        public static void SetShouldRefreshMaintenanceRepairTypes(bool arg)
        {
            _ShouldRefreshMaintenanceRepairTypes = arg;
        }


        private static bool _ShouldRefreshMeasurementUnitTypes;

        public static bool GetShouldRefreshMeasurementUnitTypes()
        {
            return _ShouldRefreshMeasurementUnitTypes;
        }
        public static void SetShouldRefreshMeasurementUnitTypes(bool arg)
        {
            _ShouldRefreshMeasurementUnitTypes = arg;
        }


        private static bool _ShouldRefreshOperationIndicators;

        public static bool GetShouldRefreshOperationIndicators()
        {
            return _ShouldRefreshOperationIndicators;
        }
        public static void SetShouldRefreshOperationIndicators(bool arg)
        {
            _ShouldRefreshOperationIndicators = arg;
        }


        private static bool _ShouldRefreshOrganizationLogos;

        public static bool GetShouldRefreshOrganizationLogos()
        {
            return _ShouldRefreshOrganizationLogos;
        }
        public static void SetShouldRefreshOrganizationLogos(bool arg)
        {
            _ShouldRefreshOrganizationLogos = arg;
        }


        private static bool _ShouldRefreshOrganizations;

        public static bool GetShouldRefreshOrganizations()
        {
            return _ShouldRefreshOrganizations;
        }
        public static void SetShouldRefreshOrganizations(bool arg)
        {
            _ShouldRefreshOrganizations = arg;
        }


        private static bool _ShouldRefreshPersonnelImages;

        public static bool GetShouldRefreshPersonnelImages()
        {
            return _ShouldRefreshPersonnelImages;
        }
        public static void SetShouldRefreshPersonnelImages(bool arg)
        {
            _ShouldRefreshPersonnelImages = arg;
        }


        private static bool _ShouldRefreshPersonnels;

        public static bool GetShouldRefreshPersonnels()
        {
            return _ShouldRefreshPersonnels;
        }
        public static void SetShouldRefreshPersonnels(bool arg)
        {
            _ShouldRefreshPersonnels = arg;
        }


        private static bool _ShouldRefreshPositions;

        public static bool GetShouldRefreshPositions()
        {
            return _ShouldRefreshPositions;
        }
        public static void SetShouldRefreshPositions(bool arg)
        {
            _ShouldRefreshPositions = arg;
        }


        private static bool _ShouldRefreshRoles;

        public static bool GetShouldRefreshRoles()
        {
            return _ShouldRefreshRoles;
        }
        public static void SetShouldRefreshRoles(bool arg)
        {
            _ShouldRefreshRoles = arg;
        }


        private static bool _ShouldRefreshUsers;

        public static bool GetShouldRefreshUsers()
        {
            return _ShouldRefreshUsers;
        }
        public static void SetShouldRefreshUsers(bool arg)
        {
            _ShouldRefreshUsers = arg;
        }


        private static bool _ShouldRefreshAssetDefectBoundAssets;

        public static bool GetShouldRefreshAssetDefectBoundAssets()
        {
            return _ShouldRefreshAssetDefectBoundAssets;
        }
        public static void SetShouldRefreshAssetDefectBoundAssets(bool arg)
        {
            _ShouldRefreshAssetDefectBoundAssets = arg;
        }


        private static bool _ShouldRefreshAssetDefects;

        public static bool GetShouldRefreshAssetDefects()
        {
            return _ShouldRefreshAssetDefects;
        }
        public static void SetShouldRefreshAssetDefects(bool arg)
        {
            _ShouldRefreshAssetDefects = arg;
        }


        private static bool _ShouldRefreshDefectremovalways;

        public static bool GetShouldRefreshDefectremovalways()
        {
            return _ShouldRefreshDefectremovalways;
        }
        public static void SetShouldRefreshDefectremovalways(bool arg)
        {
            _ShouldRefreshDefectremovalways = arg;
        }


        private static bool _ShouldRefreshDefectTypes;

        public static bool GetShouldRefreshDefectTypes()
        {
            return _ShouldRefreshDefectTypes;
        }
        public static void SetShouldRefreshDefectTypes(bool arg)
        {
            _ShouldRefreshDefectTypes = arg;
        }


        private static bool _ShouldRefreshAssetCategoryAttributeMaps;

        public static bool GetShouldRefreshAssetCategoryAttributeMaps()
        {
            return _ShouldRefreshAssetCategoryAttributeMaps;
        }
        public static void SetShouldRefreshAssetCategoryAttributeMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryAttributeMaps = arg;
        }


        private static bool _ShouldRefreshAssetCategoryCharacteristicsMaps;

        public static bool GetShouldRefreshAssetCategoryCharacteristicsMaps()
        {
            return _ShouldRefreshAssetCategoryCharacteristicsMaps;
        }
        public static void SetShouldRefreshAssetCategoryCharacteristicsMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryCharacteristicsMaps = arg;
        }


        private static bool _ShouldRefreshAssetCategoryDefectMaps;

        public static bool GetShouldRefreshAssetCategoryDefectMaps()
        {
            return _ShouldRefreshAssetCategoryDefectMaps;
        }
        public static void SetShouldRefreshAssetCategoryDefectMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryDefectMaps = arg;
        }


        private static bool _ShouldRefreshAssetCategoryDocumentTypeMaps;

        public static bool GetShouldRefreshAssetCategoryDocumentTypeMaps()
        {
            return _ShouldRefreshAssetCategoryDocumentTypeMaps;
        }
        public static void SetShouldRefreshAssetCategoryDocumentTypeMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryDocumentTypeMaps = arg;
        }


        private static bool _ShouldRefreshAssetCategoryOperationIndicatorsMaps;

        public static bool GetShouldRefreshAssetCategoryOperationIndicatorsMaps()
        {
            return _ShouldRefreshAssetCategoryOperationIndicatorsMaps;
        }
        public static void SetShouldRefreshAssetCategoryOperationIndicatorsMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryOperationIndicatorsMaps = arg;
        }


        private static bool _ShouldRefreshAssetCategoryServiceIntervalsMaps;

        public static bool GetShouldRefreshAssetCategoryServiceIntervalsMaps()
        {
            return _ShouldRefreshAssetCategoryServiceIntervalsMaps;
        }
        public static void SetShouldRefreshAssetCategoryServiceIntervalsMaps(bool arg)
        {
            _ShouldRefreshAssetCategoryServiceIntervalsMaps = arg;
        }


        private static bool _ShouldRefreshAssetEmplacements;

        public static bool GetShouldRefreshAssetEmplacements()
        {
            return _ShouldRefreshAssetEmplacements;
        }
        public static void SetShouldRefreshAssetEmplacements(bool arg)
        {
            _ShouldRefreshAssetEmplacements = arg;
        }


        private static bool _ShouldRefreshContrAgents;

        public static bool GetShouldRefreshContrAgents()
        {
            return _ShouldRefreshContrAgents;
        }
        public static void SetShouldRefreshContrAgents(bool arg)
        {
            _ShouldRefreshContrAgents = arg;
        }


        private static bool _ShouldRefreshCountrys;

        public static bool GetShouldRefreshCountrys()
        {
            return _ShouldRefreshCountrys;
        }
        public static void SetShouldRefreshCountrys(bool arg)
        {
            _ShouldRefreshCountrys = arg;
        }


        private static bool _ShouldRefreshScheduleTypes;

        public static bool GetShouldRefreshScheduleTypes()
        {
            return _ShouldRefreshScheduleTypes;
        }
        public static void SetShouldRefreshScheduleTypes(bool arg)
        {
            _ShouldRefreshScheduleTypes = arg;
        }


        private static bool _ShouldRefreshAssetTypeTemplates;

        public static bool GetShouldRefreshAssetTypeTemplates()
        {
            return _ShouldRefreshAssetTypeTemplates;
        }
        public static void SetShouldRefreshAssetTypeTemplates(bool arg)
        {
            _ShouldRefreshAssetTypeTemplates = arg;
        }


        private static bool _ShouldRefreshAssetTypeRelations;

        public static bool GetShouldRefreshAssetTypeRelations()
        {
            return _ShouldRefreshAssetTypeRelations;
        }
        public static void SetShouldRefreshAssetTypeRelations(bool arg)
        {
            _ShouldRefreshAssetTypeRelations = arg;
        }

    }
}