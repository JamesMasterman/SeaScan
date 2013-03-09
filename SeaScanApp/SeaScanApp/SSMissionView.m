//
//  SSMissionRouteView.m
//  SharkScan
//
//  Created by James Masterman on 5/11/12.
//
//

#import "SSMissionView.h"
#import "SSMissionRoutePointView.h"

@implementation SSMissionView

@synthesize route;


-(MKPolylineView*) polyLineView
{
    
    MKPolylineView* polylineView = [[MKPolylineView alloc]initWithPolyline:[self polyLine]];
    polylineView.strokeColor = colour;
    polylineView.lineWidth = lineWidth;
    polylineView.lineDashPhase = lineDashPhase;
    polylineView.lineDashPattern = dashPattern;
    
    return polylineView;
}

-(MKPolyline*) polyLine
{
    MKPolyline* pl = nil;
    
    if(route != nil && route.MissionPoints != nil && route.MissionPoints.count > 0)
    {
        pl = [MKPolyline polylineWithCoordinates:[self coords] count:route.MissionPoints.count];
        pl.title = [NSString stringWithFormat:@"%d", route.missionID];
    }
    
    return pl;
}

-(CLLocationCoordinate2D*) coords
{
    CLLocationCoordinate2D* pts = nil;
    
    if(route != nil && route.MissionPoints != nil && route.MissionPoints.count > 0)
    {
        pts = (CLLocationCoordinate2D*)malloc(route.MissionPoints.count * sizeof(CLLocationCoordinate2D));
        
        int i= 0;
        for(SSMissionRoutePointView* pt in route.MissionPoints)
        {
            pts[i] = pt.getMapCoordinate;
            i++;
        }
    }
    
    return pts;
}

-(id) initWithRouteAndPattern:(SSMission*)r colour:(UIColor*)c lineWidth:(Float32)lw lineDashPhase:(UInt32)ldp dashPatternWidth:(NSInteger)dpw
{
    self  = [super init];
    if(self)
    {
        self.route = r;
        colour = c;
        lineWidth = lw;
        lineDashPhase = ldp;
        dashPattern = [NSArray arrayWithObjects:[NSNumber numberWithInt:dpw], [NSNumber numberWithInt:dpw], nil];
    }
    return self;
   
}

-(id) initWithRoute:(SSMission*) r
{
    
    self = [super init];
    if(self)
    {
        self.route = r;
        colour = [UIColor greenColor];
        lineWidth = 2.0;
        lineDashPhase = 5;
        dashPattern = [NSArray arrayWithObjects:[NSNumber numberWithInt:5], [NSNumber numberWithInt:5], nil];
    }
    
    return self;
    
}



@end
