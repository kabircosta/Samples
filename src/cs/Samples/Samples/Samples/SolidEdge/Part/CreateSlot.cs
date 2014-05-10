﻿using ApiSamples.Samples.SolidEdge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSamples.Samples.SolidEdge.Part
{
    /// <summary>
    /// Creates a new part with a slot.
    /// </summary>
    class CreateSlot
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break();

            SolidEdgeFramework.Application application = null;
            SolidEdgePart.PartDocument partDocument = null;
            SolidEdgePart.Model model = null;
            SolidEdgePart.RefPlanes refPlanes = null;
            SolidEdgePart.RefPlane refPlane = null;
            SolidEdgePart.Sketchs sketches = null;
            SolidEdgePart.Sketch sketch = null;
            SolidEdgePart.Profiles profiles = null;
            SolidEdgePart.Profile profile = null;
            SolidEdgeFrameworkSupport.Lines2d lines2d = null;
            SolidEdgeFrameworkSupport.Line2d line2d = null;
            SolidEdgePart.Slots slots = null;
            SolidEdgePart.Slot slot = null;
            SolidEdgeFramework.SelectSet selectSet = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = ApplicationHelper.Connect(true, true);

                // Get a reference to the active document.
                partDocument = application.Documents.AddPartDocument();

                model = PartHelper.CreateSweptProtrusion(partDocument);

                // Get a reference to the RefPlanes collection.
                refPlanes = partDocument.RefPlanes;

                refPlane = refPlanes.GetRightPlane();

                // Get a reference to the Sketches collection.
                sketches = partDocument.Sketches;

                // Create a new sketch.
                sketch = sketches.Add();

                // Get a reference to the Profiles collection.
                profiles = sketch.Profiles;

                // Create a new profile.
                profile = profiles.Add(refPlane);

                // Get a reference to the Lines2d collection.
                lines2d = profile.Lines2d;

                // Add a new line.
                line2d = lines2d.AddBy2Points(0, 0, 0.01, 0.01);

                // Get a reference to the Slots collection.
                slots = model.Slots;

                // Add a new slot.
                slot = slots.Add(profile, SolidEdgePart.FeaturePropertyConstants.igRegularSlot,
                    SolidEdgePart.FeaturePropertyConstants.igFormedEnd,
                    0.01,
                    0.0,
                    0.0,
                    SolidEdgePart.FeaturePropertyConstants.igFinite,
                    SolidEdgePart.FeaturePropertyConstants.igLeft,
                    0.5,
                    SolidEdgePart.KeyPointExtentConstants.igTangentNormal,
                    null,
                    SolidEdgePart.FeaturePropertyConstants.igNone,
                    SolidEdgePart.FeaturePropertyConstants.igNone,
                    0.0,
                    SolidEdgePart.KeyPointExtentConstants.igTangentNormal,
                    null,
                    null,
                    SolidEdgePart.OffsetSideConstants.seOffsetNone,
                    0.0,
                    null,
                    SolidEdgePart.OffsetSideConstants.seOffsetNone,
                    0.0);

                // Get a reference to the ActiveSelectSet.
                selectSet = application.ActiveSelectSet;

                // Empty ActiveSelectSet.
                selectSet.RemoveAll();

                // Add new Slot to ActiveSelectSet.
                selectSet.Add(slot);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OleMessageFilter.Unregister();
            }
        }
    }
}