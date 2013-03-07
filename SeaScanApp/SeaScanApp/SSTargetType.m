//
//  SSTargetTypes.m
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import "SSTargetType.h"

@implementation SSTargetType

@synthesize targetTypeID,targetName, description;

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* targetTypesMapping = [RKObjectMapping mappingForClass:[SSTargetType class]];
    [targetTypesMapping addAttributeMappingsFromDictionary:@{
     @"ID": @"targetTypeID",
     @"TargetName": @"targetName",
     @"Description": @"description"
     }];
    
    return targetTypesMapping;
    
}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath
{
    
    //@"/service.php/targettypes"
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSTargetType getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
    
    return responseDescriptor;
    
}

@end
