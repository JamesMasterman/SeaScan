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
    MKAnnotationView* customAnnotationView;

}

-(id) initWithPoint:(SSMissionRoutePoint*)p;

@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@property (nonatomic, retain) SSMissionRoutePoint* point;
@property (nonatomic, retain) UIImage* icon;
@property (nonatomic, retain) UIImage* scannedIcon;
@property (nonatomic, readonly, copy) NSString* _title;
@property (nonatomic, readonly, copy) NSString* _subtitle;

UIImage* getMissionPointIconFromID(int tgtID);
-(CLLocationCoordinate2D) getMapCoordinate;

-(MKAnnotationView*) getAnnotationView;

@end
