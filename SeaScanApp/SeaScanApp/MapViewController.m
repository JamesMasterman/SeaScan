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
//    MKPolylineView* polylineView = [[MKPolylineView alloc]initWithPolyline:overlay];
//    polylineView.strokeColor = [UIColor cyanColor];
//    polylineView.lineWidth = 2.0;
//    polylineView.lineDashPhase = 5;
//    NSArray* array = [NSArray arrayWithObjects:[NSNumber numberWithInt:5], [NSNumber numberWithInt:5], nil];
//    polylineView.lineDashPattern = array;
   
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


//- (IBAction)filterByLocation:(id)sender
//{
//    NSLog(@"Location Filter Clicked");
//    
//    LocationListViewController* locationChooser = [[LocationListViewController alloc]init];
//    UINavigationController* navController = [[UINavigationController alloc]initWithRootViewController:locationChooser];
//    
//    [locationChooser setDismissBlock:^{
//        NSString* location = [SSConstants selectedLocation];
//        NSDate*   date     = [SSConstants earliestDate];
//        [[SSDataQuery getInstance]getFilteredMissions:location date:date];
//        [locationTitle setTitle:[SSConstants selectedLocation]];
//    }];
//    
//    [self presentViewController:navController animated:YES completion:nil];

//    
//    CLLocationCoordinate2D coord[4];
//  //  for(NSInteger i=0;i<10;i++)
//    {
//        coord[0] = CLLocationCoordinate2DMake(-32.139097, 115.759260);
//        coord[1] = CLLocationCoordinate2DMake(-32.137646, 115.748983);
//        coord[2] = CLLocationCoordinate2DMake(-32.138930, 115.748527);
//        coord[3] = CLLocationCoordinate2DMake(-32.139077, 115.756890);
//    }
//    
//    SSMissionRoutePoint* pt = [[SSMissionRoutePoint alloc]initWithCoordinate:coord[0] pointID:1 isATarget:TRUE];
//    
//    
//    SSMissionRoutePointView* pv = [[SSMissionRoutePointView alloc]initWithPoint:pt image:[UIImage imageNamed:@"shark.png"]];
  
    
//
////
////    
////    DroneRoutePoint* pt2 = [[DroneRoutePoint alloc]initWithCoordinate:CLLocationCoordinate2DMake(loc.latitude+0.001, loc.longitude)  mission:@"Mission 256 - Scarborough" pointID:@"2"];
//    
//    
//    [mapView addAnnotation:pv];
////    [mapView addAnnotation:pt2];
//    MKPolyline *polyline = [MKPolyline polylineWithCoordinates:coord count:4];
//    [mapView addOverlay:polyline];
//    
//
//    MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(coord[0], 2000,2000);
//    [mapView setRegion:region animated:NO];
//    [locationManager stopUpdatingLocation];
    

    
//}

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
            
        }
        
        customAnnotationView.annotation = annotation;
        
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
