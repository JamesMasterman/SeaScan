//
//  SSMissionStats.m
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import "SSMissionStats.h"

@implementation SSMissionStats
@synthesize missionCount,totalDistance, beachesCovered, LatestTargets;

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* mapping = [RKObjectMapping mappingForClass:[SSMissionStats class]];
    [mapping addAttributeMappingsFromDictionary:@{
     @"MissionCount": @"missionCount",
     @"TotalDistance": @"totalDistance",
     @"BeachesCovered": @"beachesCovered"
     }];
    
    RKRelationshipMapping *targetMapping = [RKRelationshipMapping relationshipMappingFromKeyPath:@"LatestTargets" toKeyPath:@"LatestTargets" withMapping:[SSLatestTargets getObjectMapping]];
    
    
    [mapping addRelationshipMappingWithSourceKeyPath:@"LatestTargets" mapping:targetMapping.mapping];
    
    return mapping;
}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath
{
    
    //@"/service.php/targettypes"
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSMissionStats getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    
    return responseDescriptor;
    
}


@end
