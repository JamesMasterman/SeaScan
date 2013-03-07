//
//  DroneRoutePoint.m
//  SharkScan
//
//  Created by James Masterman on 28/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import "SSMissionRoutePointView.h"
#import "SSTargetType.h"
#import "SSDataManager.h"


@implementation SSMissionRoutePointView

@synthesize point, icon, coordinate, title, subTitle;

-(id) initWithPoint:(SSMissionRoutePoint *)p
{
    self = [super init];
    if(self)
    {
        point = p;
        icon = getMissionPointIconFromID(p.targetTypeID);
        coordinate = CLLocationCoordinate2DMake(p.YCoord, p.XCoord);
        
        SSDataManager* dm = [SSDataManager getInstance];
        NSString* animal = @"Unknown";
        if(dm != nil && dm.targetTypes != nil)
        {
            animal = [[dm targetTypes]objectForKey:[NSNumber numberWithInteger:p.targetTypeID]];
            if(animal == nil)
            {
                animal = @"Unknown";
            }
        }
        
        title = [NSString stringWithFormat:@"%@ at %@", animal, [SSSettings convertDateToString:[p dateRecorded] formatString:@"E d-MMM-YYYY HH:mm"]];
        
        subTitle = p.annotation;
    
    }

    return self;
}

- (id)init
{
    self = [super init];
    if (self)
    {
        point = nil;
        icon = nil;
        coordinate = CLLocationCoordinate2DMake(0.0,0.0);
        title = @"";
        subTitle = @"";
    }
    
    return self;
}

UIImage* getMissionPointIconFromID(int tgtID)
{
    NSString* icon =  @"shark30.png";
    if(tgtID == WHALE)
    {
        icon = @"whale30.png";
        
    }else if(tgtID == DOLPHIN)
    {
        icon = @"dolphin30.png";
        
    }else if(tgtID == SEAL)
    {
        icon = @"seal30.png";
        
    }else if(tgtID == WIND)
    {
        icon = @"wind30.png"; //TODO: rotation and colouring for wind speed and direction
    }
    
    return [UIImage imageNamed:icon];
}

-(CLLocationCoordinate2D) getMapCoordinate
{
    if(point != nil)
    {
        return CLLocationCoordinate2DMake(point.YCoord, point.XCoord);
    }
    else
    {
        return CLLocationCoordinate2DMake(0.0, 0.0);
    }
}


@end
