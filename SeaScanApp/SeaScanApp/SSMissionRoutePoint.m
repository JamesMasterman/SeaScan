//
//  SSMissionRoutePoint.m
//  SharkScan
//
//  Created by James Masterman on 4/11/12.
//
//

#import "SSMissionRoutePoint.h"

@implementation SSMissionRoutePoint


@synthesize pointID, pointNum, XCoord, YCoord, ZCoord, isATarget, targetTypeID, annotation, imageURL, windSpeed, windBearing, dateRecorded;


-(id)initWithCoordinate:(CLLocationCoordinate2D)c pointID:(UInt32)p isATarget:(BOOL)s
{
    self = [super init];
    if (self) {
        coordinate = c;
        pointNum  = p;
        pointID = p;
        isATarget = s;
        windBearing = 0.0;
        windSpeed = 0.0;
        XCoord = c.longitude;
        YCoord = c.latitude;
        ZCoord = 0.0;
    }
    
    annotation = nil;
       
    return self;
    
}

-(id)init
{
    self = [super init];
    if (self) {
        return [self initWithCoordinate:CLLocationCoordinate2DMake(0.0, 0.0) pointID:1 isATarget:FALSE];
    }
    
    return self;
}

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* missionPointMapping = [RKObjectMapping mappingForClass:[SSMissionRoutePoint class]];
    [missionPointMapping addAttributeMappingsFromDictionary:@{
     @"ID": @"pointID",
     @"PointNum": @"pointNum",
     @"DateRecorded":@"dateRecorded",
     @"XCoord":@"XCoord",
     @"YCoord": @"YCoord",
     @"ZCoord": @"ZCoord",
     @"TargetDetected": @"isATarget",
     @"TargetTypeID":@"targetTypeID",
     @"Annotation": @"annotation",
     @"ImageURL": @"imageURL",
     @"WindSpeed": @"windSpeed",
     @"WindBearing": @"windBearing"
     }];
    
    return missionPointMapping;
}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath
{
    //@"/service.php/missions"
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSMissionRoutePoint getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    
    return responseDescriptor;
    
}



@end
