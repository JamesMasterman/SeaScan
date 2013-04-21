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


@implementation MapViewController


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
    [super viewDidLoad];
    mapView.showsUserLocation = YES;
    mapView.mapType = MKMapTypeHybrid;
    
    //init slider popup
    sliderPopUp =  [[UILabel alloc]initWithFrame:CGRectMake(timeSlider.frame.origin.x, timeSlider.frame.origin.y-20, 100, 20)];
    [sliderPopUp setTextAlignment:UITextAlignmentCenter];
    [sliderPopUp setTextColor:[UIColor whiteColor]];
    [sliderPopUp setBackgroundColor:[UIColor grayColor]];
    [sliderPopUp setFont:[UIFont systemFontOfSize:[UIFont labelFontSize]-3]];
    [sliderPopUp setAlpha:0.f];
    
    [self.view addSubview:sliderPopUp];
   
    [self updateTitles];
    
    SSDataManager* dm = [SSDataManager getInstance];
    [dm addRefreshNotifier:^(RefreshTypes type)
     {
         if(type == MISSIONS)
         {
             timeSlider.minimumValue = 0.0f;
             timeSlider.value = 0.0;
             timeSlider.maximumValue = 1.0f;
             selectedKey = [NSNumber numberWithInt:-1];
             lastSelectedKey = [NSNumber numberWithInt:-2];
             minKey = [NSNumber numberWithInt:0];
             showingSingleMission = false;
             
             [self refreshScans];
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
        
        selectedKey = [NSNumber numberWithInt:-1];
        lastSelectedKey = [NSNumber numberWithInt:-2];
        minKey      = [NSNumber numberWithInt:0];
        showingSingleMission = false;
        
        hasSetUserLocation = NO;
        
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
    if(!hasSetUserLocation)
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
        hasSetUserLocation = YES;
    }
    
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
    if((interfaceOrientation == UIInterfaceOrientationLandscapeLeft) || (interfaceOrientation == UIInterfaceOrientationLandscapeRight) || (interfaceOrientation == UIInterfaceOrientationPortrait))
    {
        return YES;
    }
    
    return NO;
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
    ScanFilterViewController* filterChooser = [[ScanFilterViewController alloc]init];
    UINavigationController* navController = [[UINavigationController alloc]initWithRootViewController:filterChooser];
    
    [filterChooser setDismissBlock:^{
        [self refreshData:nil];
        [self updateTitles];
    }];
    
    [self presentViewController:navController animated:YES completion:nil];
}



-(MKAnnotationView *)mapView:(MKMapView *)aMapView viewForAnnotation:(id<MKAnnotation>)annotation
{
    MKAnnotationView *customAnnotationView = nil;
    if(annotation != nil && [annotation isKindOfClass:[SSMissionRoutePointView class]])
    {
        SSMissionRoutePointView* vw = (SSMissionRoutePointView*)annotation;
        customAnnotationView =  vw.getAnnotationView;
    }
    
    return customAnnotationView;
    
}


- (IBAction) refreshData:(id)sender
{
    activityIndicator.startAnimating;
    
    [self performSelector: @selector(reloadMissions)
               withObject: nil
               afterDelay: 1.0];
    
}

- (IBAction) showTargetsInRange:(id)sender
{
    MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(loc, 1000, 500);
    [mapView setRegion:region animated:NO];

}

- (IBAction) sliderDidEndSliding:(id)sender
{
    selectedKey = [NSNumber numberWithInt:(int)timeSlider.value];
    NSLog(@"Selected key %@", selectedKey.stringValue);
    
    [self updateTitleFromMission:selectedKey];
    [self refreshScans];
    
}

- (IBAction) sliderValueChanged:(id)sender
{
    selectedKey = [NSNumber numberWithInt:(int)timeSlider.value];
    NSLog(@"Selected key from value %@", selectedKey.stringValue);

    UIImageView *imageView = [timeSlider.subviews objectAtIndex:2]; //get thumb location
    CGRect theRect = [self.view convertRect:imageView.frame fromView:imageView.superview];

    [sliderPopUp setFrame:CGRectMake(theRect.origin.x-22, theRect.origin.y-30, sliderPopUp.frame.size.width, sliderPopUp.frame.size.height)];

    //get the mission id to lookup
    if(selectedKey.intValue == minKey.intValue)
    {
        [sliderPopUp setText:@"All dates"];
    }
    else if(selectedKey.intValue != lastSelectedKey.intValue)
    {
        SSDataManager* dm = [SSDataManager getInstance];
        if(dm.missionList != nil && dm.missionList.count > 0)
        {
            SSMissionView* missionView = [[dm missionList]objectForKey:selectedKey];
            if(missionView != nil)
            {
                [sliderPopUp setText:[NSString stringWithFormat:@"%d. %@", [[missionView route] missionID],[SSSettings convertDateToString:[[missionView route] dateFlown] formatString:@"E d-MMM"]]];
            }
        }
    }
    
    [UIView animateWithDuration:0.5
            animations:^{
                [sliderPopUp setAlpha:0.7f];
            }
            completion:^(BOOL finished){
                     
            }];

    [timer invalidate];
    timer = nil;
    timer = [NSTimer scheduledTimerWithTimeInterval:1
                     target:self
                     selector:@selector(dePopView)
                     userInfo:nil repeats:NO];
}



- (void) updateTitleFromMission:(NSNumber*) missionID
{
    //add each mission as overlay
    SSDataManager* dm = [SSDataManager getInstance];
    if(dm.missionList != nil && dm.missionList.count > 0)
    {
        SSMissionView* missionView = [[dm missionList]objectForKey:missionID];
        
        if(missionView != nil)
        {
            SSLocation* currentLocation = [[dm locations] objectForKey:[NSNumber numberWithInt:[[missionView route]locationID]]];
            
            if(currentLocation != nil)
            {
                [headerTopLine setText:currentLocation.location];
            }
            
            [headerLowerLine setText:[NSString stringWithFormat:@"%@ %@", @"scanned",[SSSettings convertDateToString:[[missionView route] dateFlown] formatString:@"E d-MMM"]]];
        }
        
    }
}

- (void) reloadMissions
{
    SSDataManager* dm = [SSDataManager getInstance];

    [dm refreshMissions:[SSSettings selectedLocationID] earliestDate:[SSSettings earliestDate] onlyTargetLocations:[SSSettings onlyShowMissionsWithTarget]];

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



-(void) refreshScans
{
    BOOL relocated = NO;
    [mapView removeOverlays:[mapView overlays]];
    [mapView removeAnnotations:[mapView annotations]];
    
    //add each mission as overlay
    SSDataManager* dm = [SSDataManager getInstance];
    showingSingleMission = selectedKey.intValue > minKey.intValue;
    
    if(dm.missionList != nil && dm.missionList.count > 0)
    {
        for(NSNumber* mvKey in dm.missionList)
        {
            NSLog(@"Mission key %@", mvKey.stringValue);
            
            if(selectedKey.intValue < minKey.intValue)
            {
                if(mvKey.floatValue > timeSlider.maximumValue)
                {
                    [timeSlider setMaximumValue:[mvKey floatValue]];
                    NSLog(@"Maximum value %@", mvKey.stringValue);
                }
                
                if(mvKey.intValue < minKey.intValue || minKey.intValue == 0)
                {
                    [timeSlider setMinimumValue:(mvKey.floatValue-1.0)];
                    minKey = [NSNumber numberWithInt:(mvKey.intValue-1)];
                    [timeSlider setValue:(mvKey.floatValue-1.0)];
                    NSLog(@"Minimum key %@", minKey.stringValue);
                }
            }
            
            SSMissionView* missionView = [dm.missionList objectForKey:mvKey];
            NSLog(@"Printing mission mvKey = %f", mvKey.floatValue);
            
            if([[missionView route]MissionPoints] != nil)
            {
                if([[[missionView route]MissionPoints]count] > 0)
                {
                    for(SSMissionRoutePointView* rv in missionView.route.MissionPoints)
                    {
                        NSLog(@"Printing point %@ %d", rv._title, (int)rv.point.pointID);
                        MKAnnotationView* vw = [mapView viewForAnnotation:rv];
                        if(vw == nil)
                        {
                            [mapView addAnnotation:rv];
                            vw = [mapView viewForAnnotation:rv];
                        }
                        
                        [vw setHidden:NO];
                        
                        if(selectedKey.intValue > minKey.intValue)
                        {
                            if(![selectedKey isEqualToNumber:mvKey])
                            {
                                [vw setHidden:YES];
                            }
                        }
                        
                        if(!relocated && (!showingSingleMission || (showingSingleMission && [selectedKey isEqualToNumber:mvKey])))
                        {
                            loc= rv.getMapCoordinate;
                            
                            MKCoordinateRegion region = MKCoordinateRegionMakeWithDistance(loc, 1500, 1500);
                            [mapView setRegion:region animated:NO];
                            relocated = YES;
                        }
                    }
                }
            
                if(showingSingleMission)
                {
                    if([selectedKey isEqualToNumber:mvKey])
                    {
                        [mapView addOverlay:[missionView polyLine]];
                    }
                }
                else
                {
                    [mapView addOverlay:[missionView polyLine]];
                }
            }
        }
    }
    
    activityIndicator.stopAnimating;
}

- (void)dePopView{
    [UIView animateWithDuration:0.5
                     animations:^{
                         [sliderPopUp setAlpha:0.f];
                     }
                     completion:^(BOOL finished){
                         
                     }];
    
}



@end
