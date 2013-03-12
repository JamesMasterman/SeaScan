//
//  FirstViewController.m
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import "MapViewController.h"
#import "SSMissionRoutePointView.h"
#import "SSMissionRoutePoint.h"
#import "SSMissionView.h"
#import "ScanFilterViewController.h"
#import "SSDataManager.h"

@implementation MapViewController


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
    [super viewDidLoad];
    mapView.showsUserLocation = YES;
    mapView.mapType = MKMapTypeHybrid;
    
    
    self.updateTitles;
    
    SSDataManager* dm = [SSDataManager getInstance];
    [dm addRefreshNotifier:^(RefreshTypes type)
     {
         BOOL relocated = NO;
         if(type == MISSIONS)
         {
             [mapView removeOverlays:[mapView overlays]];
            //add each mission as overlay
             if(dm.missionList != nil && dm.missionList.count > 0)
             {
                 for(NSNumber* mvKey in dm.missionList)
                 {
                     NSLog(mvKey.stringValue);
                     
                     SSMissionView* missionView = [dm.missionList objectForKey:mvKey];
                    
                     if([[missionView route]MissionPoints] != nil)
                     {
                         if([[[missionView route]MissionPoints]count] > 0)
                         {
                             for(SSMissionRoutePointView* rv in missionView.route.MissionPoints)
                             {
                                 [mapView addAnnotation:rv];
                                 
                                 if(!relocated)
                                 {
                                    loc= rv.getMapCoordinate;
                                 
                                     MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(loc, 250, 250);
                                     [mapView setRegion:region animated:NO];
                                     relocated = YES;
                                 }
                             }
                             
                            

                         }
                     }
                     
                     [mapView addOverlay:[missionView polyLine]];
                 }
                 
             }
             
                          
         }
         
     }];
    
    
}

- (id)initWithNibName:(NSString* )nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    
    if(self)
    {
        locationManager = [[CLLocationManager alloc]init];

        locationManager.desiredAccuracy = kCLLocationAccuracyBest;
        locationManager.delegate = self;
        
   
        mapView.delegate = self;
        
    }
    
    return self;
}

- (void) locationManager:(CLLocationManager *)manager 
         didUpdateToLocation:(CLLocation *)newLocation 
         fromLocation:(CLLocation *)oldLocation
{
    

}

-(void) mapView:(MKMapView*)mapView didUpdateUserLocation:(MKUserLocation *)userLocation
{
    loc = userLocation.coordinate;
    
    /*MKCoordinateRegion newRegion;
    newRegion.center.latitude = 37.786996;
    newRegion.center.longitude = -122.440100;
    newRegion.span.latitudeDelta = 0.112872;
    newRegion.span.longitudeDelta = 0.109863;*/
    
    MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(loc, 250, 250);
    [mapView setRegion:region animated:NO];
    NSLog(@"Updated user location");
    
}

-(MKOverlayView*) mapView:(MKMapView *)mapView viewForOverlay:(id <MKOverlay>)overlay
{
    MKOverlayView* view = nil;
    if(overlay != nil)
    {
        NSString* missionID = [overlay title];
        
        SSMissionView* mrv = [[[SSDataManager getInstance]missionList] objectForKey:[NSNumber numberWithInteger:[missionID integerValue]]];
        
        view = [mrv polyLineView];
    }
   
    return view;
}


-(void) locationManager:(CLLocationManager*)manager didFailWithError:(NSError *)error
{
    NSLog(@"Could not find location %@",error.description);
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc. that aren't in use.
}

- (void)viewDidUnload
{
    [super viewDidUnload];

    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

-(void) dealloc
{
    locationManager.delegate = nil;
}

- (IBAction)filterScans:(id)sender
{
    NSLog(@" Filter clicked");
    ScanFilterViewController* dateChooser = [[ScanFilterViewController alloc]init];
    UINavigationController* navController = [[UINavigationController alloc]initWithRootViewController:dateChooser];
    
    [dateChooser setDismissBlock:^{
        self.updateTitles;
    }];
    
    [self presentViewController:navController animated:YES completion:nil];
}



-(MKAnnotationView *)mapView:(MKMapView *)aMapView viewForAnnotation:(id<MKAnnotation>)annotation
{
    MKAnnotationView *customAnnotationView = nil;
    if(annotation != nil && [annotation isKindOfClass:[SSMissionRoutePointView class]])
    {
        static NSString *reuseId = @"customAnn";
        
        customAnnotationView = [mapView dequeueReusableAnnotationViewWithIdentifier:reuseId];
        
        if (customAnnotationView == nil)
        {
            customAnnotationView = [[MKAnnotationView alloc] initWithAnnotation:annotation reuseIdentifier:reuseId];
            
            SSMissionRoutePointView* vw = (SSMissionRoutePointView*)annotation;
            if(vw.icon != nil)
            {
                [customAnnotationView setImage:vw.icon];
            }
            
            if(vw.scannedIcon !=nil)
            {
                UIImageView *imageView=[[UIImageView alloc] initWithImage:vw.scannedIcon];
                imageView.frame=CGRectMake(0.0, 0.0, 32.0, 32.0);
                imageView.contentMode = UIViewContentModeScaleAspectFit;
                customAnnotationView.leftCalloutAccessoryView=imageView;
                
            }
        }
        
        customAnnotationView.annotation = annotation;
        customAnnotationView.canShowCallout = YES;
        
        /* code for showing image in callout
         UIImage *flagImage = [UIImage imageNamed:@"flag.png"];
         
         // size the flag down to the appropriate size
         CGRect resizeRect;
         resizeRect.size = flagImage.size;
         CGSize maxSize = CGRectInset(self.view.bounds,
         [MapViewController annotationPadding],
         [MapViewController annotationPadding]).size;
         maxSize.height -= self.navigationController.navigationBar.frame.size.height + [MapViewController calloutHeight];
         if (resizeRect.size.width > maxSize.width)
         resizeRect.size = CGSizeMake(maxSize.width, resizeRect.size.height / resizeRect.size.width * maxSize.width);
         if (resizeRect.size.height > maxSize.height)
         resizeRect.size = CGSizeMake(resizeRect.size.width / resizeRect.size.height * maxSize.height, maxSize.height);
         
         resizeRect.origin = CGPointMake(0.0, 0.0);
         UIGraphicsBeginImageContext(resizeRect.size);
         [flagImage drawInRect:resizeRect];
         UIImage *resizedImage = UIGraphicsGetImageFromCurrentImageContext();
         UIGraphicsEndImageContext();
         
         annotationView.image = resizedImage;
         annotationView.opaque = NO;
         
         UIImageView *sfIconView = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"SFIcon.png"]];*/
         
         
       /*  // offset the flag annotation so that the flag pole rests on the map coordinate
         annotationView.centerOffset = CGPointMake( annotationView.centerOffset.x + annotationView.image.size.width/2, annotationView.centerOffset.y - annotationView.image.size.height/2 );
         
         return annotationView;*/
    }
    
    return customAnnotationView;
    
}


- (IBAction) refreshData:(id)sender
{
    SSDataManager* dm = [SSDataManager getInstance];
    
    [dm refreshMissions:[SSSettings selectedLocationID] earliestDate:[SSSettings earliestDate] onlyTargetLocations:[SSSettings onlyShowMissionsWithTarget]];
    
}

- (IBAction) showTargetsInRange:(id)sender
{
    MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(loc, 1000, 500);
    [mapView setRegion:region animated:NO];

}

- (void) updateTitles
{
    int locID = [SSSettings selectedLocationID];
    
    if(locID > 0)
    {
        SSLocation* currentLocation = nil;
        
        if([[SSDataManager getInstance]locations] != nil)
        {
            currentLocation = [[[SSDataManager getInstance]locations] objectForKey:[NSNumber numberWithInt:locID]];
        }
        
        if(currentLocation != nil)
        {
            [headerTopLine setText:currentLocation.location];
        }
    }

    
    [headerLowerLine setText:[NSString stringWithFormat:@"%@ %@", @"since",[SSSettings convertDateToString:[SSSettings earliestDate] formatString:@"E d-MMM"]]];
    
}

@end
