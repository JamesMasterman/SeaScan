//
//  SSMissionRoutePointView
//  SharkScan
//
//  Created by James Masterman on 28/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>
#import "SSMissionRoutePoint.h"


@interface SSMissionRoutePointView : NSObject <MKAnnotation>
{
   

}

-(id) initWithPoint:(SSMissionRoutePoint*)p;

@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@property  SSMissionRoutePoint* point;
@property  UIImage* icon;
@property (nonatomic, readonly, copy) NSString *title;
@property (nonatomic, readonly, copy) NSString *subTitle;

UIImage* getMissionPointIconFromID(int tgtID);
-(CLLocationCoordinate2D) getMapCoordinate;

@end
