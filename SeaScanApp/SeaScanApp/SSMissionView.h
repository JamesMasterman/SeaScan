//
//  SSMissionRouteView.h
//  SharkScan
//
//  Created by James Masterman on 5/11/12.
//
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>
#import "SSMission.h"

@interface SSMissionView : NSObject
{
    UIColor* colour;
    Float32 lineWidth;
    UInt32 lineDashPhase;
    NSArray* dashPattern;

}

@property (nonatomic, copy) SSMission* route;


 
-(MKPolylineView*) polyLineView;
-(MKPolyline*) polyLine;


-(id) initWithRouteAndPattern:(SSMission*)r colour:(UIColor*)c lineWidth:(Float32)lw lineDashPhase:(UInt32)ldp dashPatternWidth:(NSInteger)dpw;
-(id) initWithRoute:(SSMission*) r;
-(CLLocationCoordinate2D*) coords;




@end
