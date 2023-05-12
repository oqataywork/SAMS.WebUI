using System;
using System.Collections.Generic;
using System.Linq;
using SAMS.Context;
using SAMS.Core.Helpers;
using SAMS.Entities;
using SAMS.Model;

namespace SAMS.WebUI.Services
{
	

            //private static bool _ShouldRefreshAssetAttributeCategorys;

            //internal static bool GetShouldRefreshAssetAttributeCategorys()
            //{
            //    return _ShouldRefreshAssetAttributeCategorys;
            //}
            //internal static void SetShouldRefreshAssetAttributeCategorys(bool arg)
            //{
            //    _ShouldRefreshAssetAttributeCategorys=arg;
            //}


            //private static bool _ShouldRefreshAssetAttributeMaps;

            //internal static bool GetShouldRefreshAssetAttributeMaps()
            //{
            //    return _ShouldRefreshAssetAttributeMaps;
            //}
            //internal static void SetShouldRefreshAssetAttributeMaps(bool arg)
            //{
            //    _ShouldRefreshAssetAttributeMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetAttributes;

            //internal static bool GetShouldRefreshAssetAttributes()
            //{
            //    return _ShouldRefreshAssetAttributes;
            //}
            //internal static void SetShouldRefreshAssetAttributes(bool arg)
            //{
            //    _ShouldRefreshAssetAttributes=arg;
            //}


            //private static bool _ShouldRefreshAssetCategorys;

            //internal static bool GetShouldRefreshAssetCategorys()
            //{
            //    return _ShouldRefreshAssetCategorys;
            //}
            //internal static void SetShouldRefreshAssetCategorys(bool arg)
            //{
            //    _ShouldRefreshAssetCategorys=arg;
            //}


            //private static bool _ShouldRefreshAssetCharacteristics;

            //internal static bool GetShouldRefreshAssetCharacteristics()
            //{
            //    return _ShouldRefreshAssetCharacteristics;
            //}
            //internal static void SetShouldRefreshAssetCharacteristics(bool arg)
            //{
            //    _ShouldRefreshAssetCharacteristics=arg;
            //}


            //private static bool _ShouldRefreshAssetDocuments;

            //internal static bool GetShouldRefreshAssetDocuments()
            //{
            //    return _ShouldRefreshAssetDocuments;
            //}
            //internal static void SetShouldRefreshAssetDocuments(bool arg)
            //{
            //    _ShouldRefreshAssetDocuments=arg;
            //}


            //private static bool _ShouldRefreshAssets;

            //internal static bool GetShouldRefreshAssets()
            //{
            //    return _ShouldRefreshAssets;
            //}
            //internal static void SetShouldRefreshAssets(bool arg)
            //{
            //    _ShouldRefreshAssets=arg;
            //}


            //private static bool _ShouldRefreshAssetServiceIntervals;

            //internal static bool GetShouldRefreshAssetServiceIntervals()
            //{
            //    return _ShouldRefreshAssetServiceIntervals;
            //}
            //internal static void SetShouldRefreshAssetServiceIntervals(bool arg)
            //{
            //    _ShouldRefreshAssetServiceIntervals=arg;
            //}


            //private static bool _ShouldRefreshAssetStatusHistorys;

            //internal static bool GetShouldRefreshAssetStatusHistorys()
            //{
            //    return _ShouldRefreshAssetStatusHistorys;
            //}
            //internal static void SetShouldRefreshAssetStatusHistorys(bool arg)
            //{
            //    _ShouldRefreshAssetStatusHistorys=arg;
            //}


            //private static bool _ShouldRefreshAssetTypeImages;

            //internal static bool GetShouldRefreshAssetTypeImages()
            //{
            //    return _ShouldRefreshAssetTypeImages;
            //}
            //internal static void SetShouldRefreshAssetTypeImages(bool arg)
            //{
            //    _ShouldRefreshAssetTypeImages=arg;
            //}


            //private static bool _ShouldRefreshAssetTypes;

            //internal static bool GetShouldRefreshAssetTypes()
            //{
            //    return _ShouldRefreshAssetTypes;
            //}
            //internal static void SetShouldRefreshAssetTypes(bool arg)
            //{
            //    _ShouldRefreshAssetTypes=arg;
            //}


            //private static bool _ShouldRefreshCharacteristicCategorys;

            //internal static bool GetShouldRefreshCharacteristicCategorys()
            //{
            //    return _ShouldRefreshCharacteristicCategorys;
            //}
            //internal static void SetShouldRefreshCharacteristicCategorys(bool arg)
            //{
            //    _ShouldRefreshCharacteristicCategorys=arg;
            //}


            //private static bool _ShouldRefreshCharacteristics;

            //internal static bool GetShouldRefreshCharacteristics()
            //{
            //    return _ShouldRefreshCharacteristics;
            //}
            //internal static void SetShouldRefreshCharacteristics(bool arg)
            //{
            //    _ShouldRefreshCharacteristics=arg;
            //}


            //private static bool _ShouldRefreshDepartments;

            //internal static bool GetShouldRefreshDepartments()
            //{
            //    return _ShouldRefreshDepartments;
            //}
            //internal static void SetShouldRefreshDepartments(bool arg)
            //{
            //    _ShouldRefreshDepartments=arg;
            //}


            //private static bool _ShouldRefreshDocumentTypes;

            //internal static bool GetShouldRefreshDocumentTypes()
            //{
            //    return _ShouldRefreshDocumentTypes;
            //}
            //internal static void SetShouldRefreshDocumentTypes(bool arg)
            //{
            //    _ShouldRefreshDocumentTypes=arg;
            //}


            //private static bool _ShouldRefreshLocations;

            //internal static bool GetShouldRefreshLocations()
            //{
            //    return _ShouldRefreshLocations;
            //}
            //internal static void SetShouldRefreshLocations(bool arg)
            //{
            //    _ShouldRefreshLocations=arg;
            //}


            //private static bool _ShouldRefreshMaintenanceRepairTypes;

            //internal static bool GetShouldRefreshMaintenanceRepairTypes()
            //{
            //    return _ShouldRefreshMaintenanceRepairTypes;
            //}
            //internal static void SetShouldRefreshMaintenanceRepairTypes(bool arg)
            //{
            //    _ShouldRefreshMaintenanceRepairTypes=arg;
            //}


            //private static bool _ShouldRefreshMeasurementUnitTypes;

            //internal static bool GetShouldRefreshMeasurementUnitTypes()
            //{
            //    return _ShouldRefreshMeasurementUnitTypes;
            //}
            //internal static void SetShouldRefreshMeasurementUnitTypes(bool arg)
            //{
            //    _ShouldRefreshMeasurementUnitTypes=arg;
            //}


            //private static bool _ShouldRefreshOperationIndicators;

            //internal static bool GetShouldRefreshOperationIndicators()
            //{
            //    return _ShouldRefreshOperationIndicators;
            //}
            //internal static void SetShouldRefreshOperationIndicators(bool arg)
            //{
            //    _ShouldRefreshOperationIndicators=arg;
            //}


            //private static bool _ShouldRefreshOrganizationLogos;

            //internal static bool GetShouldRefreshOrganizationLogos()
            //{
            //    return _ShouldRefreshOrganizationLogos;
            //}
            //internal static void SetShouldRefreshOrganizationLogos(bool arg)
            //{
            //    _ShouldRefreshOrganizationLogos=arg;
            //}


            //private static bool _ShouldRefreshOrganizations;

            //internal static bool GetShouldRefreshOrganizations()
            //{
            //    return _ShouldRefreshOrganizations;
            //}
            //internal static void SetShouldRefreshOrganizations(bool arg)
            //{
            //    _ShouldRefreshOrganizations=arg;
            //}


            //private static bool _ShouldRefreshPersonnelImages;

            //internal static bool GetShouldRefreshPersonnelImages()
            //{
            //    return _ShouldRefreshPersonnelImages;
            //}
            //internal static void SetShouldRefreshPersonnelImages(bool arg)
            //{
            //    _ShouldRefreshPersonnelImages=arg;
            //}


            //private static bool _ShouldRefreshPersonnels;

            //internal static bool GetShouldRefreshPersonnels()
            //{
            //    return _ShouldRefreshPersonnels;
            //}
            //internal static void SetShouldRefreshPersonnels(bool arg)
            //{
            //    _ShouldRefreshPersonnels=arg;
            //}


            //private static bool _ShouldRefreshPositions;

            //internal static bool GetShouldRefreshPositions()
            //{
            //    return _ShouldRefreshPositions;
            //}
            //internal static void SetShouldRefreshPositions(bool arg)
            //{
            //    _ShouldRefreshPositions=arg;
            //}


            //private static bool _ShouldRefreshRoles;

            //internal static bool GetShouldRefreshRoles()
            //{
            //    return _ShouldRefreshRoles;
            //}
            //internal static void SetShouldRefreshRoles(bool arg)
            //{
            //    _ShouldRefreshRoles=arg;
            //}


            //private static bool _ShouldRefreshUsers;

            //internal static bool GetShouldRefreshUsers()
            //{
            //    return _ShouldRefreshUsers;
            //}
            //internal static void SetShouldRefreshUsers(bool arg)
            //{
            //    _ShouldRefreshUsers=arg;
            //}


            //private static bool _ShouldRefreshAssetDefectBoundAssets;

            //internal static bool GetShouldRefreshAssetDefectBoundAssets()
            //{
            //    return _ShouldRefreshAssetDefectBoundAssets;
            //}
            //internal static void SetShouldRefreshAssetDefectBoundAssets(bool arg)
            //{
            //    _ShouldRefreshAssetDefectBoundAssets=arg;
            //}


            //private static bool _ShouldRefreshAssetDefects;

            //internal static bool GetShouldRefreshAssetDefects()
            //{
            //    return _ShouldRefreshAssetDefects;
            //}
            //internal static void SetShouldRefreshAssetDefects(bool arg)
            //{
            //    _ShouldRefreshAssetDefects=arg;
            //}


            //private static bool _ShouldRefreshDefectremovalways;

            //internal static bool GetShouldRefreshDefectremovalways()
            //{
            //    return _ShouldRefreshDefectremovalways;
            //}
            //internal static void SetShouldRefreshDefectremovalways(bool arg)
            //{
            //    _ShouldRefreshDefectremovalways=arg;
            //}


            //private static bool _ShouldRefreshDefectTypes;

            //internal static bool GetShouldRefreshDefectTypes()
            //{
            //    return _ShouldRefreshDefectTypes;
            //}
            //internal static void SetShouldRefreshDefectTypes(bool arg)
            //{
            //    _ShouldRefreshDefectTypes=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryAttributeMaps;

            //internal static bool GetShouldRefreshAssetCategoryAttributeMaps()
            //{
            //    return _ShouldRefreshAssetCategoryAttributeMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryAttributeMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryAttributeMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryCharacteristicsMaps;

            //internal static bool GetShouldRefreshAssetCategoryCharacteristicsMaps()
            //{
            //    return _ShouldRefreshAssetCategoryCharacteristicsMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryCharacteristicsMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryCharacteristicsMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryDefectMaps;

            //internal static bool GetShouldRefreshAssetCategoryDefectMaps()
            //{
            //    return _ShouldRefreshAssetCategoryDefectMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryDefectMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryDefectMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryDocumentTypeMaps;

            //internal static bool GetShouldRefreshAssetCategoryDocumentTypeMaps()
            //{
            //    return _ShouldRefreshAssetCategoryDocumentTypeMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryDocumentTypeMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryDocumentTypeMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryOperationIndicatorsMaps;

            //internal static bool GetShouldRefreshAssetCategoryOperationIndicatorsMaps()
            //{
            //    return _ShouldRefreshAssetCategoryOperationIndicatorsMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryOperationIndicatorsMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryOperationIndicatorsMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetCategoryServiceIntervalsMaps;

            //internal static bool GetShouldRefreshAssetCategoryServiceIntervalsMaps()
            //{
            //    return _ShouldRefreshAssetCategoryServiceIntervalsMaps;
            //}
            //internal static void SetShouldRefreshAssetCategoryServiceIntervalsMaps(bool arg)
            //{
            //    _ShouldRefreshAssetCategoryServiceIntervalsMaps=arg;
            //}


            //private static bool _ShouldRefreshAssetEmplacements;

            //internal static bool GetShouldRefreshAssetEmplacements()
            //{
            //    return _ShouldRefreshAssetEmplacements;
            //}
            //internal static void SetShouldRefreshAssetEmplacements(bool arg)
            //{
            //    _ShouldRefreshAssetEmplacements=arg;
            //}


            //private static bool _ShouldRefreshContrAgents;

            //internal static bool GetShouldRefreshContrAgents()
            //{
            //    return _ShouldRefreshContrAgents;
            //}
            //internal static void SetShouldRefreshContrAgents(bool arg)
            //{
            //    _ShouldRefreshContrAgents=arg;
            //}


            //private static bool _ShouldRefreshCountrys;

            //internal static bool GetShouldRefreshCountrys()
            //{
            //    return _ShouldRefreshCountrys;
            //}
            //internal static void SetShouldRefreshCountrys(bool arg)
            //{
            //    _ShouldRefreshCountrys=arg;
            //}


            //private static bool _ShouldRefreshScheduleTypes;

            //internal static bool GetShouldRefreshScheduleTypes()
            //{
            //    return _ShouldRefreshScheduleTypes;
            //}
            //internal static void SetShouldRefreshScheduleTypes(bool arg)
            //{
            //    _ShouldRefreshScheduleTypes=arg;
            //}


            //private static bool _ShouldRefreshAssetTypeTemplates;

            //internal static bool GetShouldRefreshAssetTypeTemplates()
            //{
            //    return _ShouldRefreshAssetTypeTemplates;
            //}
            //internal static void SetShouldRefreshAssetTypeTemplates(bool arg)
            //{
            //    _ShouldRefreshAssetTypeTemplates=arg;
            //}


            //private static bool _ShouldRefreshAssetTypeRelations;

            //internal static bool GetShouldRefreshAssetTypeRelations()
            //{
            //    return _ShouldRefreshAssetTypeRelations;
            //}
            //internal static void SetShouldRefreshAssetTypeRelations(bool arg)
            //{
            //    _ShouldRefreshAssetTypeRelations=arg;
            //}


            //private static bool _ShouldRefreshAssetOperationIndicatorsMaps;

            //internal static bool GetShouldRefreshAssetOperationIndicatorsMaps()
            //{
            //    return _ShouldRefreshAssetOperationIndicatorsMaps;
            //}
            //internal static void SetShouldRefreshAssetOperationIndicatorsMaps(bool arg)
            //{
            //    _ShouldRefreshAssetOperationIndicatorsMaps=arg;
            //}


}
