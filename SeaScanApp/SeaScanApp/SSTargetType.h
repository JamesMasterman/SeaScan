//
//  SSTargetTypes.h
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import <Foundation/Foundation.h>
#import <RestKit/RestKit.h>
#import <RestKit/CoreData.h>

typedef NS_ENUM(NSInteger, TargetTypes) {
    SHARK = 6,
    WHALE = 3,
    DOLPHIN = 4,
    SEAL = 5,
    WIND = 1
};

@interface SSTargetType : NSObject
{

}

@property (nonatomic, assign) int targetTypeID;
@property (nonatomic, copy)   NSString* targetName;
@property (nonatomic, copy) NSString* description;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath;

@end
