#include "PerformanceBed.h"

PerformanceBed::PerformanceBed( vector<Robot> &_robots) : robots(_robots) {

}

PerformanceBed::~PerformanceBed(void)
{
	for (int i=0; i < pm.size(); i++) 
	{
		if (pm[i]) {
			delete pm[i];
			pm[i] = 0;
		}
	}
}

 void PerformanceBed::trigger() 
 {
	//for each performance measure
	for (int i(0); i < pm.size(); i++) 
	{
		pm[i]->takeMeasure(robots);
	}

 }

void PerformanceBed::attach( PerformanceMeasure* _pm ) {
	pm.push_back(_pm);
}

void PerformanceBed::finalize() {
	//for each performance measure
	for (int i(0); i < pm.size(); i++) {
		pm[i]->finalize();
	}	
}

