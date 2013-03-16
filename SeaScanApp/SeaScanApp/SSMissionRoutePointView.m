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

@synthesize point, icon, scannedIcon, coordinate, _title, _subtitle;

-(id) initWithPoint:(SSMissionRoutePoint *)p
{
    self = [super init];
    if(self)
    {
        point = p;
        icon = getMissionPointIconFromID(p.targetTypeID);
        coordinate = CLLocationCoordinate2DMake(p.YCoord, p.XCoord);
        scannedIcon = nil;
        
        SSDataManager* dm = [SSDataManager getInstance];
        NSString* animal = @"Unknown";
        _title = @"";
        _subtitle = @"";
        
        if(dm != nil && dm.targetTypes != nil)
        {
            if(p.targetTypeID != WIND)
            {
                SSTargetType* targetType = [[dm targetTypes]objectForKey:[NSNumber numberWithInteger:p.targetTypeID]];
            
                if(targetType != nil)
                {
                    animal = targetType.targetName;
                }
            
        
        
                _title = animal;
        
                _subtitle = [NSString stringWithFormat:@"%@. Scanned %@", p.annotation, [SSSettings convertDateToString:[p dateRecorded] formatString:@"E d-MMM-YYYY h:mma"]];
            }
        }
        
        if(p.isATarget && p.imageURL != nil && p.imageURL.length > 0) //get the image
        {
            NSString* imagePath = [NSString stringWithFormat:@"%@/%@", dm.baseURL, p.imageURL];
          //  NSURL *url = [NSURL URLWithString:imagePath];
           // NSData *imageData = [NSData dataWithContentsOfURL:url];
            //scannedImage = [[UIImage alloc]initWithData:imageData];
            
           // imagePath = [imagePath stringByAppendingString:[[UIScreen mainScreen] scale] > 1 ? @"@2x.png": @".png"];
            
            NSData *imageData = [[NSData alloc] initWithContentsOfURL: [NSURL URLWithString:imagePath]];
            
            CGDataProviderRef dataProvider = CGDataProviderCreateWithCFData((__bridge CFDataRef)imageData);
            CGImageRef imageRef = CGImageCreateWithPNGDataProvider(dataProvider, NULL, NO, kCGRenderingIntentDefault);
            scannedIcon = [UIImage imageWithCGImage:imageRef scale:[[UIScreen mainScreen] scale] orientation:UIImageOrientationUp];
            
            CGDataProviderRelease(dataProvider);
            CGImageRelease(imageRef);
            
        }
        
        //setup the custom annotation view
        NSString *reuseId = @"customAnn";
        reuseId = [NSString stringWithFormat:@"%d_%d", point.parentMissionID, (int)point.pointID];
        
        customAnnotationView = [[MKAnnotationView alloc] initWithAnnotation:self reuseIdentifier:reuseId];
            
        if(icon != nil)
        {
            [customAnnotationView setImage:icon];
        }
        
        if(scannedIcon !=nil)
        {
            UIImageView *imageView=[[UIImageView alloc] initWithImage:scannedIcon];
            imageView.frame=CGRectMake(0.0, 0.0, 32.0, 32.0);
            imageView.contentMode = UIViewContentModeScaleAspectFit;
            customAnnotationView.leftCalloutAccessoryView=imageView;
            
        }
       
        
        customAnnotationView.annotation = self;
        customAnnotationView.canShowCallout = YES;
        
    
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
        _title = @"";
        _subtitle = @"";
    }
    
    return self;
}

-(MKAnnotationView*) getAnnotationView
{
    return customAnnotationView;
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

- (NSString *)title
{
    return _title;
}

- (NSString *)subtitle
{
    return _subtitle;
}


@end
