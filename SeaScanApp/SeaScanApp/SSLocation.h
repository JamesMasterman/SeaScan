//
//  SSLocation.h
//  SharkScan
//
//  Created by James Masterman on 11/11/12.
//
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>
#import <RestKit/RestKit.h>
#import <RestKit/CoreData.h>

#define ALL_LOCATIONS @"All"

@interface SSLocation : NSObject
{
}

@property (nonatomic, assign) int locID;
@property (nonatomic, copy)   NSString* location;
@property (nonatomic, assign) double minX;
@property (nonatomic, assign) double maxX;
@property (nonatomic, assign) double minY;
@property (nonatomic, assign) double maxY;


-(CLLocationCoordinate2D) getMinCoord;
-(CLLocationCoordinate2D) getMaxCoord;
-(BOOL) pointInLocation:(CLLocationCoordinate2D)pt;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*) responsePath;

@end
