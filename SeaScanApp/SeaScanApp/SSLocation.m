//
//  SSLocation.m
//  SharkScan
//
//  Created by James Masterman on 11/11/12.
//
//

#import "SSLocation.h"
#import <RestKit/RKObjectMapping.h>
@implementation SSLocation

@synthesize locID,location, minX, minY, maxX, maxY;



-(BOOL) pointInLocation:(CLLocationCoordinate2D)pt
{
   BOOL isInLocation = FALSE;
    
   if(pt.latitude >= minY && pt.latitude <= maxY)
   {
       if(pt.longitude >= minX && pt.longitude <= minX)
       {
           isInLocation = TRUE;
       }
   }
    
    return isInLocation;
}

-(CLLocationCoordinate2D) getMinCoord
{
    return CLLocationCoordinate2DMake(minY, minX);
}

-(CLLocationCoordinate2D) getMaxCoord
{
    return CLLocationCoordinate2DMake(maxY, maxX);
}

+(RKObjectMapping*) getObjectMapping
{
    RKObjectMapping* locationMapping = [RKObjectMapping mappingForClass:[SSLocation class]];
    [locationMapping addAttributeMappingsFromDictionary:@{
     @"ID": @"locID",
     @"LocationName": @"location",
     @"MinX": @"minX",
     @"MinY": @"minY",
     @"MaxX": @"maxX",
     @"MaxY": @"maxY"
     }];

    return locationMapping;

}

+(RKResponseDescriptor*) getResponseDescriptor:(NSString*) responsePath
{
        
    
    RKResponseDescriptor *responseDescriptor = [RKResponseDescriptor responseDescriptorWithMapping:[SSLocation getObjectMapping] pathPattern:responsePath keyPath:nil statusCodes:RKStatusCodeIndexSetForClass(RKStatusCodeClassSuccessful)];
//@"/service.php/locations/iphone_app/f085278a8c60148dd4ab31a682a67be5/all/false/false"
    return responseDescriptor;
    
}

- (NSComparisonResult)compare:(SSLocation *)otherObject {
    
    return [self.location compare:otherObject.location];
}

@end
