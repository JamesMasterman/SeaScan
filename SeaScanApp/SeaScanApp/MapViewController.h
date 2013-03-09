//
//  FirstViewController.h
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>

@interface MapViewController : UIViewController
<CLLocationManagerDelegate, MKMapViewDelegate>
{
    CLLocationManager* locationManager;
    
    __weak IBOutlet MKMapView* mapView;
    __weak IBOutlet UIBarButtonItem* locationTitle;
    __weak IBOutlet UIButton* showCloseOnlyButton;
    __weak IBOutlet UILabel* headerTopLine;
    __weak IBOutlet UILabel* headerLowerLine;
    __weak IBOutlet UISlider* timeSlider;
    
    CLLocationCoordinate2D loc;
    
    
}
- (IBAction)filterScans:(id)sender;
- (IBAction) refreshData:(id)sender;
- (IBAction) showTargetsInRange:(id)sender;

- (void) updateTitles;



@end
