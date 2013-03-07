//
//  SSMissionRoutePoint.h
//  SharkScan
//
//  Created by James Masterman on 4/11/12.
//
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>
#import <RestKit/CoreData.h>


@interface SSMissionRoutePoint : NSObject
{
    CLLocationCoordinate2D coordinate;

}
-(id)initWithCoordinate:(CLLocationCoordinate2D)c pointID:(UInt32)p isATarget:(BOOL)s;


@property (nonatomic) UInt32 pointID;
@property (nonatomic) UInt32 pointNum;
@property (nonatomic) double XCoord;
@property (nonatomic) double YCoord;
@property (nonatomic) double ZCoord;
@property (nonatomic) BOOL isATarget;
@property (nonatomic) int targetTypeID;
@property (nonatomic, copy) NSString* annotation;
@property (nonatomic, copy) NSString* imageURL;
@property (nonatomic) double windSpeed;
@property (nonatomic) double windBearing;
@property (nonatomic) NSDate* dateRecorded;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath;

@end
