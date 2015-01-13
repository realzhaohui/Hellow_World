using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;

namespace CommonClassLibrary
{
   public class RasterCalculator
    {
       ////栅格计算例子
       //private static void RasterCalculate(string fileName)
       //{
       //    IRasterLayer rl1 = axMapControl1.get_Layer(0) as IRasterLayer;
       //    IRasterLayer rl2 = axMapControl1.get_Layer(1) as IRasterLayer;
       //    IRaster r1 = rl1.Raster;
       //    IRaster r2 = rl2.Raster;
       //    IGeoDataset g1 = r1 as IGeoDataset;
       //    IGeoDataset g2 = r2 as IGeoDataset;
       //    IMapAlgebraOp mp = new RasterMapAlgebraOpClass();
       //    mp.BindRaster(g1, "g1");
       //    mp.BindRaster(g2, "g2");
       //    IGeoDataset g = mp.Execute("[g1] * 10 + [g2]");
       //    IWorkspaceFactory pWKSF = new RasterWorkspaceFactoryClass();
       //    IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
       //    ISaveAs pSaveAs = g as ISaveAs;
       //    pSaveAs.SaveAs(System.IO.Path.GetFileName(fileName), pWorkspace, "IMAGINE Image");//以img格式保存
       //    IRasterLayer r = new RasterLayerClass();
       //    r.CreateFromRaster(g as IRaster);
       //    axMapControl1.AddLayer(r);
       //}

       public static IGeoDataset RasterCalculate(IRasterLayer[] rasterLayers, string expression, string outRasterPath)
       {
           IMapAlgebraOp mapAlgebraOp = new RasterMapAlgebraOpClass();
           IRaster raster;
           IGeoDataset geoDataset;
           for(int i=0;i<rasterLayers.Length;i++)
           {
               raster = rasterLayers[i].Raster;
               geoDataset = raster as IGeoDataset;
               mapAlgebraOp.BindRaster(geoDataset, rasterLayers[i].Name);
           }
           IGeoDataset resultGeoDataset = mapAlgebraOp.Execute(expression);
           IWorkspaceFactory pWKSF = new RasterWorkspaceFactory();
           IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(outRasterPath), 0);
           string rasterFormat = "IMAGINE Image";
           if (outRasterPath.EndsWith(".tif"))
               rasterFormat = "TIFF";
           ISaveAs pSaveAs = resultGeoDataset as ISaveAs;
           pSaveAs.SaveAs(System.IO.Path.GetFileName(outRasterPath), pWorkspace, rasterFormat);
           //System.Threading.Thread.Sleep(5000);
           return resultGeoDataset;
           //以img格式保存
       }
       //
       public static IGeoDataset RasterCalculate(IRasterLayer[] rasterLayers, string expression)
       {
           IMapAlgebraOp mapAlgebraOp = new RasterMapAlgebraOpClass();
           IRaster raster;
           IGeoDataset geoDataset;
           for (int i = 0; i < rasterLayers.Length; i++)
           {
               raster = rasterLayers[i].Raster;
               geoDataset = raster as IGeoDataset;
               mapAlgebraOp.BindRaster(geoDataset, rasterLayers[i].Name);
           }
           IGeoDataset resultGeoDataset = mapAlgebraOp.Execute(expression);
           return resultGeoDataset;
           //以img格式保存
       }
       //
       public static void RasterCalculate(IGeoDataset geoDataset, string expression, string fileName)
       {
           IMapAlgebraOp mapAlgebraOp = new RasterMapAlgebraOpClass();
           mapAlgebraOp.BindRaster(geoDataset, "p");
           IGeoDataset resultGeoDataset = mapAlgebraOp.Execute(expression);
           IWorkspaceFactory pWKSF = new RasterWorkspaceFactory();
           IWorkspace pWorkspace = pWKSF.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
           string rasterFormat = "IMAGINE Image";
           if (fileName.EndsWith(".tif"))
               rasterFormat = "TIFF";
           ISaveAs pSaveAs = resultGeoDataset as ISaveAs;
           pSaveAs.SaveAs(System.IO.Path.GetFileName(fileName), pWorkspace, rasterFormat);
           //以img格式保存
       }
       //将栅格添加到地图中
       public static void AddRasterToMap(AxMapControl mapcontrol,IGeoDataset geoDataset,string rasterName)
       {
           IRasterLayer rasterLayer = new RasterLayerClass();
           rasterLayer.CreateFromRaster(geoDataset as IRaster);
           rasterLayer.Name = rasterName;
           mapcontrol.AddLayer(rasterLayer);
       }
    }
}
