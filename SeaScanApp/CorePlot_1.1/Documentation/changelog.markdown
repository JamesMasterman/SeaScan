# Release 1.1 (November 11, 2012)

## Release Notes

## Details
- **New**: Added Bezier curve interpolation option to `CPTScatterPlot`.
- **New**: Added printing support to the OS X hosting view.
- **New**: Added a plot data label selection delegate method.
- **New**: Added a line fill property to `CPTLineStyle`.
- **New**: Added point selection delegate methods to `CPTRangePlot` and `CPTTradingRangePlot`.
- **New**: Added an end angle property and the ability to rotate data labels relative to the pie radius to `CPTPieChart`.
- **New**: Added the `-didFinishDrawing:` delegate method to `CPTPlot`.
- **New**: Added selection delegate methods that include the event that triggered the selection to all plots.
- **New**: Added plot space convenience methods that convert global interaction coordinates obtained from an OS event to plot area drawing coordinates.
- **New**: Added the plot symbol anchor point.
- **New**: Added a corner radius property to `CPTBarPlot` that applies to the base value end of the bars.
- **New**: Added a method to `CPTBarPlot` that computes a plot range that encloses all of the bars, including the bar width and bar offset.
- **New**: Added bindings for plot data labels, bar plot fills and borders, and pie chart fills and radial offsets.
- **New**: Added bindings and data source methods for all line styles and fills in range and trading range plots.
- **New**: Added additional data source methods to all plots so each data source item can be supplied one at a time or in an array.
- **New**: Added multi-dimensional data array support to `CPTNumericData`.
- **New**: Added a new plot datasource method that returns a multi-dimensional `CPTNumericData` containing data for all plot fields.
- **Changed**: Changed the superclass of `CPTTextLayer` from `CPTLayer` to `CPTBorderedLayer`.
- **Changed**: Changed all iOS header files to "Project" visibility to prevent problems with archiving an app that uses the Core Plot static library.
- **Changed**: Significant updates to the API documentation for organization and clarity.
- **Changed**: Miscellaneous bug fixes and cleanup.



# Release 1.0 (February 20, 2012)

## Release Notes

This release contains changes that will break apps built against earlier Core Plot versions. `CPTPlotRange` has been split into mutable and immutable variants similar to `CPTLineStyle` and `CPTTextStyle`. This change will break existing code that changes plot ranges; that code should be updated to use the mutable versions. The `CPTBarPlotField` enum was changed so that the field values start at zero like all other plots. This will affect any existing code that used hard-coded values for the fields instead of the enum field names (`CPTBarPlotFieldBarLocation`, etc.). The `CPTNewCGColorFromNSColor()` function was renamed to `CPTCreateCGColorFromNSColor()` to conform to Apple's memory management naming conventions.

Core Plot now supports projects that use automatic reference counting (ARC). For compatibility with older systems, Core Plot itself does not use ARC but its header files and binaries can be used in applications that do.

## Details
- **New**: Added a script to automatically format the code following the coding standards using uncrustify.
- **New**: Added support for automatic reference counting (ARC).
- **New**: Added the HTML docs built with Doxygen to the source code repository so that they can be viewed online with a web browser.
- **New**: Added an `identifier` property to `CPTLayer`, replacing the one from `CPTPlot` and `CPTPlotGroup`.
- **New**: Added control chart and multi-colored bar plot examples to the Plot Gallery app.
- **New**: Added a mouse zoom user interface to the DropPlot app.
- **Changed**: Split `CPTPlotRange` into mutable and immutable variants.
- **Changed**: Changed the `CPTBarPlotField` enum values.
- **Changed**: Renamed the `CPTNewCGColorFromNSColor()` function to `CPTCreateCGColorFromNSColor()`.
- **Changed**: Updated all projects to support Xcode 4.2.
- **Changed**: Made all `CPTAxis` delegate methods optional.
- **Changed**: Added the `-[CPTAxis axis:shouldUpdateMinorAxisLabelsAtLocations:]` delegate method.
- **Changed**: Added a graph fill to the Slate theme so that all themes have explicit fills.
- **Changed**: Miscellaneous bug fixes and cleanup.
- **Removed**: Removed unused `NSException` extensions.
- **Removed**: Removed unused methods from `CPTLayer` and `CPTTextLayer`.



# Release 0.9 Beta (September 25, 2011)

## Release Notes

This release contains several changes that will break apps built against earlier Core Plot versions. The MacOS hosting view has been renamed and many deprecated properties and methods have been removed. The axis constraints system has been changed; the new class-based system is easier to set up and less prone to problems than the old struct-based system. This release also adds support for many new features including hi-dpi displays on MacOS, @2x images on both iOS and MacOS, shadows on many plot elements, arrows and other line caps on axis lines, and animation support for many plot properties.

## Details

- **New**: Added support for hi-dpi displays in MacOS 10.7 Lion.
- **New**: Added support for @2x images on hi-dpi displays.
- **New**: Added support for shadows.
- **New**: Added arrows and other line caps for axis lines.
- **New**: Added animation support for many plot properties.
- **New**: Added a `shape` property setter to `CPTMutableNumericData`.
- **New**: Implemented the `<NSCoding>` protocol in all Core Plot classes.
- **New**: Added several new plots to the Plot Gallery example application.
- **New**: Added some unit tests.
- **Changed**: Renamed the MacOS hosting view to `CPTGraphHostingView` to match the iOS nomenclature.
- **Changed**: Renamed the `CPTBarPlot` `barLengths` property to `barTips` to conform with the field naming convention (location, tip, and base).
- **Changed**: Changed the axis constraints mechanism--it now uses a class instead of a struct.
- **Changed**: Made plot point pixel alignment optional.
- **Changed**: Made the header files for several internal classes private.
- **Changed**: Performance enhancements in several plots and `CPTXYAxis`.
- **Changed**: Updated all projects to support Xcode 4.1.
- **Changed**: Miscellaneous bug fixes and cleanup.
- **Removed**: Removed the unused `CPTLayoutManager` protocol.
- **Removed**: Removed the unused `CPTPolarPlotSpace` class.
- **Removed**: Removed deprecated properties and methods.
- **Removed**: Removed the unused default z-position from all layers.



# Release 0.4 Alpha (July 7, 2011)

## Release Notes

This release contains several changes that will break apps built against earlier Core Plot versions. The class prefix for all Core Plot classes and public symbols changed from "CP" to "CPT". Also, the iOS SDK is no longer supported. iOS projects that were built against the SDK must be updated to include the Core Plot project file or link against the new universal static library instead. 

## Details

- **New**: Added iOS universal static library build target.
- **New**: Added graph legends.
- **New**: Implemented logarithmic plot scales.
- **New**: Added support for multi-line text in `CPTTextLayer`.
- **New**: Added a text alignment property to `CPTTextStyle`.
- **New**: Added a title rotation property to `CPTAxis`.
- **New**: Added several new plots to the Plot Gallery example application.
- **New**: Added an equal interval axis labeling policy.
- **Changed**: Changed the class prefix from "CP" to "CPT" to prevent conflicts with Apple's Core PDF framework.
- **Changed**: Updates to suppress compiler warnings when using the LLVM compiler.
- **Changed**: Miscellaneous bug fixes and cleanup.
- **Removed**: Removed the iOS SDK build and installer packaging scripts.



# Release 0.3 Alpha (May 4, 2011)

## Release Notes

This release adds a new plot type (range plots) and adds features to several other plot types. `CPTextStyle` and `CPLineStyle` have been split into mutable and immutable variants similar to Cocoa's string, data, and collection classes. This change will break existing code that changes text style and line style properties; that code should be updated to use the mutable versions.

## Details

- **New**: Added an option in `CPBarPlot` to set bar widths in data or view coordinates.
- **New**: Added the `allowPinchScaling` property to `CPGraphHostingView`.
- **New**: Added a shared cache of common values to the `NSDecimal` conversion utility functions for performance.
- **New**: Added support for variable bar plot base values, useful for making stacked plots.
- **New**: Added range plot type.
- **New**: Added minor axis tick labels.
- **New**: Added pie chart overlay fill.
- **New**: Added some unit tests.
- **Changed**: Split `CPTextStyle` and `CPLineStyle` into mutable and immutable variants.
- **Changed**: Updated all projects to support Xcode 4.
- **Changed**: Drawing performance improvements in `CPBarPlot`.
- **Changed**: Updated Doxygen configuration files and comments for Doxygen 1.7.3.
- **Changed**: Miscellaneous bug fixes and cleanup.



# Release 0.2.2 Alpha (November 10, 2010)

## Release Notes

This is a maintenance release that corrects some packaging errors in release 0.2.1. It also adds support for missing data in all plot types.

## Details

- **New**: Added support for missing plot data points.
- **New**: Added histogram interpolation to `CPScatterPlot`.
- **New**: Added secondary scatter plot fill.
- **Changed**: Updated the release packaging scripts.
- **Changed**: Miscellaneous bug fixes and cleanup.



# Release 0.2.1 Alpha (October 20, 2010)

## Release Notes

This release adds methods to improve plot performance by only updating the parts of the data cache that changed. It also adds support for Xcode 3.2.4 and the iOS 4.1 SDK.

## Details

- **New**: Added packaging scripts for releases.
- **New**: Added plot methods to add, remove, and reload plot data records without reloading all of the data.
- **New**: Added support for axis label alignment.
- **Changed**: Updated all projects to support Xcode 3.2.4 and the iOS 4.1 SDK.
- **Changed**: Optimized plot symbol drawing.
- **Changed**: Miscellaneous bug fixes and cleanup.
- **Removed**: Removed the Core Plot animation classes.



# Release 0.2 Alpha (September 10, 2010)

## Release Notes

This release adds data labels to all plots and adds features to scatter plots and pie charts. It also makes substantial improvements to `CPNumericData` and modifies the plots to use it for the internal data cache. All C++ code has been removed from Core Plot, which improves performance and simplifies the process of linking Core Plot into other projects.

## Details

- **New**: Added plot data labels.
- **New**: Added stepped interpolation to `CPScatterPlot`.
- **New**: Added `NSDecimal` support to `CPNumericData`.
- **New**: Added `NSDecimal` utility functions for all primitive types supported by `NSNumber`.
- **New**: Added slice selection delegate method to `CPPieChart`.
- **New**: Added some unit tests.
- **Changed**: Updated the example apps to demonstrate new features.
- **Changed**: Changed the plot data cache to use `CPNumericData`.
- **Changed**: Miscellaneous bug fixes and cleanup.
- **Removed**: Removed all C++ code.



# Release 0.1 Alpha (June 20, 2010)

## Release Notes

First official release package.

## Details

- **New**: Implemented basic graph architecture, axes, and plots.
- **New**: Added several example applications.
- **New**: Added `masksToBorder` property to `CPBorderedLayer`.
- **New**: Implemented automatic and locations provided axis labeling policies.
- **New**: Added Quartz Composer plugin.
- **New**: Added documentation targets and Doxygen comments to all classes.
- **New**: Added unit tests.
- **New**: Implemented themes.
