//
//  SSLatestTargets.m
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import "SSLatestTargets.h"

@implementation SSLatestTargets
@synthesize targetTypeID, missionID, dateRecorded, locationID;

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* mapping = [RKObjectMapping mappingForClass:[SSLatestTargets class]];
    [mapping addAttributeMappingsFromDictionary:@{
     @"TargetTypeID": @"targetTypeID",
     @"MissionID": @"missionID",
     @"DateRecorded": @"dateRecorded",
     @"LocationID":@"locationID"
     }];
    
    
    NSDateFormatter* dateFormatter = [NSDateFormatter new];
    [dateFormatter  setDateFormat:@"yyyy-MM-dd HH:mm:ss"];
    dateFormatter.timeZone = [NSTimeZone timeZoneWithAbbreviation:@"SGT"]; //use singapore offset as there is no australian one
    
    
    mapping.dateFormatters = [NSArray arrayWithObject: dateFormatter];
    return mapping;
    
}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*) responsePath
{
    
    //@"/service.php/targettypes"
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSLatestTargets getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    
    return responseDescriptor;
    
}

@end
