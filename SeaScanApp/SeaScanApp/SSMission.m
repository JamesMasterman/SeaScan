//
//  SSMission.m
//  SharkScan
//
//  Created by James Masterman on 4/11/12.
//
//

#import "SSMission.h"


@implementation SSMission

@synthesize locationID, dateFlown, numTargetsSpotted, MissionPoints, comments, missionID, duration, pointCount, distanceFlown;

-(id) initWithMission:(SSLocation*)l dateFlown:(NSDate*)d targetsSpotted:(NSInteger)ts route:(NSMutableArray*)r comments:(NSString*)c missionID:(UInt32)mid distance:(Float32)dist
{
    self = [super init];
    if(self)
    {
        self.locationID = l.locID;
        self.dateFlown = d;
        self.numTargetsSpotted = ts;
        self.MissionPoints = r;
        self.comments = c;
        self.missionID = mid;
        self.distanceFlown = dist;
        self.duration = 0.0;
        self.pointCount = ts;
        
    }
    
    return self;
}

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* missionMapping = [RKObjectMapping mappingForClass:[SSMission class]];
  
    [missionMapping addAttributeMappingsFromDictionary:@{
     @"ID": @"missionID",
     @"LocationID": @"locationID",
     @"DateFlown":@"dateFlown",
     @"Duration": @"duration",
     @"DistanceFlown": @"distanceFlown",
     @"TargetsDetected": @"numTargetsSpotted",
     @"PointCount":@"pointCount",
     @"Description": @"comments"
     }];
    
    RKRelationshipMapping *missionPointMapping = [RKRelationshipMapping relationshipMappingFromKeyPath:@"MissionPoints" toKeyPath:@"MissionPoints" withMapping:[SSMissionRoutePoint getObjectMapping]];
    
    [missionMapping addRelationshipMappingWithSourceKeyPath:@"MissionPoints" mapping:missionPointMapping.mapping];

    
   // [missionMapping addRelationshipMappingWithSourceKeyPath:@"MissionPoints" mapping:[SSMissionRoutePoint getObjectMapping]];
    
    return missionMapping;
}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath
{
    
    //@"/service.php/missions"
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSMission getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    
    return responseDescriptor;
    
}

@end
